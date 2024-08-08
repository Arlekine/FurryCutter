using System;
using System.Collections.Generic;
using FurryCutter.Gameplay.Session.ServiceLocator;
using UnityEngine;

namespace FurryCutter.Bonuses
{
    public abstract class BonusConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;

        public Sprite Icon => _icon;
        public abstract BonusType Type { get; }
        public abstract Func<IServiceLocator<ISessionService>, IBonusPlacer> GetPlacerCreator();
        public abstract Func<Transform, GameObject> GetPreviewCreator();
    }
}