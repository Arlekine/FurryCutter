using System;
using System.Collections.Generic;
using DataSystem;
using FurryCutter.Bonuses;
using FurryCutter.Gameplay.Session.ServiceLocator;

namespace FurryCutter.Application.Repositories
{
    public class BonusesRepository : IDisposable
    {
        protected DataContext _dataContext;

        private List<Bonus> _bonusesData = new List<Bonus>();

        public BonusesRepository(DataContext dataContext, BonusesHolder data)
        {
            _dataContext = dataContext;
            AddBonusToList(data.LinesCuttingConfig);
            AddBonusToList(data.StarLinesCuttingConfig);
            AddBonusToList(data.TemporalFieldConfig);
        }

        public IReadOnlyList<Bonus> GetAll() => _bonusesData;

        public IReadOnlyList<IBonusPlacer> CreateAllPlacer(IServiceLocator<ISessionService> sessionServiceLocator)
        {
            var bonusPlacer = new List<IBonusPlacer>();

            foreach (var bonus in GetAll())
            {
                var placerCreator = bonus.CreateBonusPlacer;
                bonusPlacer.Add(placerCreator(sessionServiceLocator));
            }

            return bonusPlacer;
        }

        public Bonus GetByType(BonusType type)
        {
            var entity = _bonusesData.Find(x => x.Type == type);

            if (entity == null)
                throw new ArgumentException($"No data for entity with type {type}");

            return entity;
        }

        public void Dispose()
        {
            foreach (var bonusData in _bonusesData)
            {
                bonusData.DataUpdated -= OnDataUpdated;
            }
        }

        private void AddBonusToList(BonusConfig config)
        {
            var bonusID = config.Type.ToString();

            if (_dataContext.CurrentGameData.BoughtBonuses == null)
            {
                _dataContext.CurrentGameData.BoughtBonuses = new QuantityDataHolder();
                _dataContext.MarkDirty();
            }

            if (_dataContext.CurrentGameData.BoughtBonuses.HasEntity(bonusID) == false)
            {
                _dataContext.CurrentGameData.BoughtBonuses.AddEntity(bonusID, 50);
                _dataContext.MarkDirty();
            }

            var bonus = new Bonus(config.Icon, config.GetPlacerCreator(), config.GetPreviewCreator(), _dataContext.CurrentGameData.BoughtBonuses.GetQuantity(bonusID), config.Type);
            bonus.DataUpdated += OnDataUpdated;
            _bonusesData.Add(bonus);
        }

        private void OnDataUpdated(Bonus bonus, int count)
        {
            var bonusID = bonus.Type.ToString();
            if (_dataContext.CurrentGameData.BoughtBonuses.HasEntity(bonusID) == false)
                _dataContext.CurrentGameData.BoughtBonuses.AddEntity(bonusID, count);

            _dataContext.CurrentGameData.BoughtBonuses.SetQuantity(bonusID, count);
            _dataContext.MarkDirty();
        }
    }
}