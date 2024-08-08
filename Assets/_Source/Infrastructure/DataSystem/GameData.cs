using System;
using System.Collections.Generic;

namespace DataSystem
{
    [Serializable]
    public class GameData
    {
        public int Record = 0;
        public int CurrentCoins = 0;

        public SettingsData SettingsData = new SettingsData();
        public List<string> BoughtFurries = new List<string>();
        public QuantityDataHolder BoughtBonuses = new QuantityDataHolder();
    }
}