using System;
using FurryCutter.Gameplay.Session.ServiceLocator;
using UnityEngine;

namespace FurryCutter.Bonuses
{
    public class Bonus
    {
        private BonusType _type;
        private Sprite _mainIcon;
        private Func<IServiceLocator<ISessionService>, IBonusPlacer> _createBonusPlacer;
        private Func<Transform, GameObject> _createBonusPreview;

        private int _count;

        public event Action<Bonus, int> DataUpdated;

        public Bonus(Sprite mainIcon, Func<IServiceLocator<ISessionService>, IBonusPlacer> createBonusPlacer, Func<Transform, GameObject> createBonusPreview, int count, BonusType type)
        {
            _type = type;
            _mainIcon = mainIcon;
            _createBonusPlacer = createBonusPlacer;
            _createBonusPreview = createBonusPreview;
            _count = count;
        }

        public BonusType Type => _type;
        public Sprite MainIcon => _mainIcon;
        public Func<IServiceLocator<ISessionService>, IBonusPlacer> CreateBonusPlacer => _createBonusPlacer;
        public Func<Transform, GameObject> CreateBonusPreview => _createBonusPreview;
        public int Count => _count;
        public bool CanSpend => _count > 0;

        public void Spend()
        {
            if (CanSpend == false)
                throw new ArgumentException();

            _count--;
            DataUpdated?.Invoke(this, _count);
        }

        public void Add(int amountToAdd)
        {
            if (amountToAdd <= 0)
                throw new ArgumentException("Adding amount should be positive");

            _count += amountToAdd;
            DataUpdated?.Invoke(this, _count);
        }
    }
}