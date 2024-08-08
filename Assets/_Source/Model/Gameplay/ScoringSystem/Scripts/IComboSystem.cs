using System;
using FurryCutter.Gameplay.Session.ServiceLocator;
using UnityEngine;

namespace FurryCutter.Gameplay.ScoringSystem
{
    public interface IComboSystem : ISessionService
    {
        event Action<int, int> ComboMade;
    }
}
