using YG;

namespace DataSystem
{
    public class YgDataContext : DataContext
    {
        protected override bool HasSavedData() => YandexGame.savesData.GameData != null;
        protected override GameData LoadData() => YandexGame.savesData.GameData;

        protected override void Save()
        {
            if (CurrentGameData != null && YandexGame.savesData.GameData != CurrentGameData)
                YandexGame.savesData.GameData = CurrentGameData;

            YandexGame.SaveProgress();
        }
    }
}