using System;
using System.Collections.Generic;
using System.Linq;
using DataSystem;
using FurryCutter.Meta;

namespace FurryCutter.Application.Repositories
{
    public sealed class FurriesRepository : IDisposable
    {
        protected DataContext _dataContext;

        private List<Furry> _commonFurries = new List<Furry>();
        private List<Furry> _rareFurries = new List<Furry>();
        private List<Furry> _epicFurries = new List<Furry>();

        private FurryGroupShopSettings _commonShopSettings;
        private FurryGroupShopSettings _rareShopSettings;
        private FurryGroupShopSettings _epicShopSettings;

        public FurriesRepository(DataContext dataContext, AllFurriesConfig config)
        {
            _dataContext = dataContext;

            SetUpFurryGroup(config.CommonFurries, _commonFurries);
            SetUpFurryGroup(config.RareFurries, _rareFurries);
            SetUpFurryGroup(config.EpicFurries, _epicFurries);
        }

        public FurryGroupShopSettings CommonShopSettings => _commonShopSettings;
        public FurryGroupShopSettings RareShopSettings => _rareShopSettings;
        public FurryGroupShopSettings EpicShopSettings => _epicShopSettings;

        public IEnumerable<Furry> CommonFurries => _commonFurries;
        public IEnumerable<Furry> RareFurries => _rareFurries;
        public IEnumerable<Furry> EpicFurries => _epicFurries;

        public IEnumerable<Furry> GetAll()
        {
            var allFurries = new List<Furry>();
            allFurries.AddRange(_commonFurries);
            allFurries.AddRange(_rareFurries);
            allFurries.AddRange(_epicFurries);
            return allFurries;
        }

        public IEnumerable<Furry> GetAllOpened()
        {
            return GetAll().Select(x => x).Where(x => x.IsOpened);
        }

        public Furry GetByID(FurryID id)
        {
            var entity = ((List<Furry>)GetAll()).Find(x => x.Id == id);
            if (entity == null)
                throw new ArgumentException($"No data for entity with furry {id}");

            return entity;
        }

        public IEnumerable<FurryRandomable> GetOpenedRandomables()
        {
            var randomables = new List<FurryRandomable>();
            foreach (var furry in GetAllOpened())
            {
                randomables.Add(new FurryRandomable(furry.CreateGameplayObject, furry.SpawnWeight));
            }

            return randomables;
        }

        public void Dispose()
        {
            foreach (var furry in GetAll())
            {
                furry.DataUpdated -= OnDataUpdated;
            }
        }

        private void SetUpFurryGroup(FurryGroupConfig groupConfig, List<Furry> targetList)
        {
            _commonShopSettings = new FurryGroupShopSettings(groupConfig.Cost, groupConfig.Color);

            foreach (var furryConfigs in groupConfig.Furries)
            {
                bool isInDataList = _dataContext.CurrentGameData.BoughtFurries.Contains(furryConfigs.Id.ToString());

                if (furryConfigs.IsOpenedByDefault && isInDataList == false)
                {
                    _dataContext.MarkDirty();
                }

                bool isOpened = furryConfigs.IsOpenedByDefault || isInDataList;

                var newEntity = new Furry(furryConfigs.Id, furryConfigs.Sprite, furryConfigs.GameplayPrefab,
                    groupConfig.SpawnWeight, groupConfig.ScoreForCut, isOpened);
                newEntity.DataUpdated += OnDataUpdated;

                targetList.Add(newEntity);
            }
        }

        private void OnDataUpdated(Furry furry, bool isOpened)
        {
            var idFormat = furry.Id.ToString();
            if (_dataContext.CurrentGameData.BoughtFurries.Contains(idFormat) != isOpened)
            {
                if (isOpened)
                    _dataContext.CurrentGameData.BoughtFurries.Remove(idFormat);
                else
                    _dataContext.CurrentGameData.BoughtFurries.Add(idFormat);

                _dataContext.MarkDirty();
            }
        }
    }
}