using UnityEngine;

namespace DataSystem
{
    public abstract class DataContext : MonoBehaviour
    {
        private GameData _currentGameData;
        private bool _isDirty;

        public GameData CurrentGameData => _currentGameData;

        public GameData Load(GameData initialGameData)
        {
            if (HasSavedData())
            {
                _currentGameData = LoadData();
            }
            else
            {
                _currentGameData = initialGameData;
                Save();
            }

            return _currentGameData;
        }

        public void MarkDirty() => _isDirty = true;

        protected void LateUpdate()
        {
            if (_isDirty)
            {
                _isDirty = false;
                Save();
            }
        }

        protected abstract void Save();
        protected abstract bool HasSavedData();
        protected abstract GameData LoadData();
    }
}