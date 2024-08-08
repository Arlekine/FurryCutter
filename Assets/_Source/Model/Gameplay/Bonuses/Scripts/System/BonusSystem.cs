using System.Collections.Generic;
using System.Linq;
using FurryCutter.Gameplay.Session.ServiceLocator;
using UnityEngine;

namespace FurryCutter.Bonuses
{
    public class BonusSystem : IBonusSystem
    {
        private readonly IReadOnlyList<IBonusPlacer> _bonuses;

        public BonusSystem(IReadOnlyList<IBonusPlacer> bonuses)
        {
            _bonuses = bonuses;
        }

        public void PlaceBonus(BonusType type, Vector3 point)
        {
            _bonuses.First(x => x.Type == type).Place(point);
        }
    }
}