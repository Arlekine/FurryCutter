using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace RandomSpawnSystem
{
    public static class WeightedRandomizer
    {
        public static T GetRandom<T>(IEnumerable<T> randomables) where T : IWeightedRandomable
        {
            var sumWeight = 0f;
            var currentRandomables = new List<T>(randomables);
            var maxWeight = randomables.Max(x => x.GetNormalizedSpawnWeight());

            foreach (var levelObject in randomables)
            {
                if (levelObject.GetNormalizedSpawnWeight() == 0)
                    currentRandomables.Remove(levelObject);
            }

            foreach (var levelObject in currentRandomables)
            {
                sumWeight += (maxWeight * levelObject.GetNormalizedSpawnWeight());
            }

            var randomValue = Random.Range(0f, sumWeight);
            var progress = 0f;

            foreach (var levelObject in currentRandomables)
            {
                progress += (maxWeight * levelObject.GetNormalizedSpawnWeight());
                if (randomValue <= progress)
                {
                    return levelObject;
                }
            }

            throw new Exception("Unexpected random spawning");
        }
    }
}