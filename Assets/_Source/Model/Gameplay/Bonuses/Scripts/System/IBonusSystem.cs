using FurryCutter.Gameplay.Session.ServiceLocator;
using UnityEngine;

namespace FurryCutter.Bonuses
{
    public interface IBonusSystem : ISessionService
    {
        void PlaceBonus(BonusType type, Vector3 point);
    }
}