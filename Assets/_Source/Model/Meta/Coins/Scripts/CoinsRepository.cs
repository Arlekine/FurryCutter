using System;
using DataSystem;
using MetaGameplay;

namespace FurryCutter.Application.Repositories
{
    public class CoinsRepository : IDisposable
    {
        protected DataContext _dataContext;
        
        private Currency _currency;

        public CoinsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;

            _currency = new Currency(_dataContext.CurrentGameData.CurrentCoins);
            _currency.ValueChanged += OnDataUpdated;
        }

        public Currency Get() => _currency;

        public void Dispose()
        {
            _currency.ValueChanged -= OnDataUpdated;
        }

        private void OnDataUpdated(int coins)
        {
            _dataContext.CurrentGameData.CurrentCoins = coins;
            _dataContext.MarkDirty();
        }
    }
}