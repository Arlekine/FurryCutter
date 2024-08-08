using UnityEngine;

namespace DataSystem
{
    public class LocalDataContext : DataContext
    {
        private const string DataPathName = "GameData";

        protected override bool HasSavedData() => PlayerPrefs.HasKey(DataPathName);
        protected override GameData LoadData() => JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(DataPathName));
        protected override void Save() => PlayerPrefs.SetString(DataPathName, JsonUtility.ToJson(CurrentGameData));
    }
}