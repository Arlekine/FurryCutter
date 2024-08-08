using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FurryCutter.Meta
{
    [CreateAssetMenu(menuName = "Data/Meta/FurryGroupConfig", fileName = "FurryGroupConfig")]
    public class FurryGroupConfig : ScriptableObject
    {
        [SerializeField] private int _scoreForCut = 10;
        [SerializeField] private int _cost = 100;
        [SerializeField] private Color _color = Color.white;
        [SerializeField][Range(0, 1)] private float _spawnWeight = 0.5f;
        [SerializeField] private List<FurryConfig> _furries = new List<FurryConfig>();

        public int ScoreForCut => _scoreForCut;
        public int Cost => _cost;
        public Color Color => _color;
        public float SpawnWeight => _spawnWeight;
        public IReadOnlyList<FurryConfig> Furries => _furries;


        private void OnValidate()
        {
            List<FurryID> duplicates = _furries.GroupBy(x => x.Id)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();

            foreach (var duplicate in duplicates)
            {
                Debug.LogError(duplicate.ToString());
            }
        }
    }
}