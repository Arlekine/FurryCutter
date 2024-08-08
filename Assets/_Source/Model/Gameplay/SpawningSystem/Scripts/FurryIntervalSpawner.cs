using System;
using System.Collections.Generic;
using FurryCutter.Gameplay.Session.ServiceLocator;
using FurryCutter.Meta;
using RandomSpawnSystem;
using UnityEngine;

namespace FurryCutter.Gameplay.SpawningSystem
{
    public class FurryIntervalSpawner : ISpawningSystem, IDisposable
    {
        private ITimeTrigger _timeTrigger;
        private IEnumerable<FurryRandomable> _furries;
        private SessionScene _sessionScene;
        private ValueRange _randomRotationRange;

        public FurryIntervalSpawner(IServiceLocator<ISessionService> sessionServiceLocator, ValueRange randomRotationRange, ITimeTrigger timeTrigger, IEnumerable<FurryRandomable> furries)
        {
            _timeTrigger = timeTrigger;
            _furries = furries;
            _randomRotationRange = randomRotationRange;
            _sessionScene = sessionServiceLocator.GetService<SessionScene>();

            _timeTrigger.Triggered += Spawn;
        }

        public void Dispose()
        {
            _timeTrigger.Triggered -= Spawn;
        }

        private void Spawn()
        {
            var furryToSpawn = WeightedRandomizer.GetRandom(_furries);
            var spawn = furryToSpawn.CreateGameplayObject(_sessionScene.SceneSessionParent);

            spawn.transform.position = _sessionScene.SpawnPoint.position;
            spawn.transform.eulerAngles = new Vector3(0f, 0f, _randomRotationRange.GetRandom());
        }
    }
}