using System;

namespace DataSystem
{
    [Serializable]
    public class QuantityData
    {
        public string EntityID;
        public int EntityCount;

        public QuantityData(string entityId, int entityCount)
        {
            EntityID = entityId;
            EntityCount = entityCount;
        }
    }
}