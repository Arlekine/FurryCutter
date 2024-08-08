using System;
using UnityEngine;

namespace FurryCutter.Meta
{
    [CreateAssetMenu(menuName = "Data/Meta/LevelRewardRule", fileName = "LevelRewardRule")]
    public class LevelRewardRule : ScriptableObject
    {
        [SerializeField] private float _scoreToCoinsMultiplayer = 0.25f;

        public int GetLevelReward(int score)
        {
            var coins = score * 0.25f;
            return (int)(Mathf.Round(coins / 10f) * 10f);
        }
    }
}
