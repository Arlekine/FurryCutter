using UnityEngine;

namespace FurryCutter.Gameplay.Session.ServiceLocator
{
    public class SessionScene : MonoBehaviour, ISessionService
    {
        [SerializeField] private Transform _sceneSessionParent;
        [SerializeField] private Transform _spawnPoint;

        public Transform SceneSessionParent => _sceneSessionParent;
        public Transform SpawnPoint => _spawnPoint;
    }
}