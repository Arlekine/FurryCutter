using UnityEngine;

namespace FurryCutter.Bonuses
{
    public interface IBonusPlacer
    {
        BonusType Type { get; }
        void Place(Vector3 point);
    }
}