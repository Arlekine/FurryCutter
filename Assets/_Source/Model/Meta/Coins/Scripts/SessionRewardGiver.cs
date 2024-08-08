using MetaGameplay;

namespace FurryCutter.Meta
{
    public class SessionRewardGiver
    {
        private LevelRewardRule _rewardRule;
        private Currency _coins;

        public SessionRewardGiver(LevelRewardRule rewardRule, Currency coins)
        {
            _rewardRule = rewardRule;
            _coins = coins;
        }

        public int GetCoinsForScore(int finalScore, bool isDouble = false)
        {
            var coins = _rewardRule.GetLevelReward(finalScore);
            if (isDouble)
                coins *= 2;

            return coins;
        }

        public void GiveReward(int finalScore, bool isDouble = false)
        {
            var coinsToGive = GetCoinsForScore(finalScore, isDouble);
            _coins.Add(coinsToGive);
        }
    }
}