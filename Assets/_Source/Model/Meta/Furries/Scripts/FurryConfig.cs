using FurryCutter.Gameplay.ScoringSystem;
using UnityEngine;
using UnitySpriteCutter;


namespace FurryCutter.Meta
{
    [CreateAssetMenu(menuName = "Data/Meta/Furries", fileName = "Furries")]
    public class FurryConfig : ScriptableObject
    {
        [SerializeField] private FurryID _id;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private ScoreHolder _gameplayPrefab;
        [SerializeField] private bool _isOpenedByDefault;

        public FurryID Id => _id;
        public Sprite Sprite => _sprite;
        public ScoreHolder GameplayPrefab => _gameplayPrefab;
        public bool IsOpenedByDefault => _isOpenedByDefault;
    }
}
