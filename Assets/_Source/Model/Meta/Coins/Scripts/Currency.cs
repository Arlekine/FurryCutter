using System;

namespace MetaGameplay
{
    public class Currency
    {
        private int _currentValue;

        public Currency(int initialValue)
        {
            _currentValue = initialValue;
        }

        public event Action<int, int> Added;
        public event Action<int, int> Spent;
        public event Action<int> ValueChanged;

        public int CurrentValue => _currentValue;

        public void Add(int valueToAdd)
        {
            if (valueToAdd <= 0)
                throw new ArgumentException($"It is possible to add only positive value to {nameof(this.GetType)}");

            _currentValue += valueToAdd;
            Added?.Invoke(valueToAdd, _currentValue);
            ValueChanged?.Invoke(_currentValue);
        }

        public void Spend(int valueToSpend)
        {
            if (valueToSpend <= 0)
                throw new ArgumentException($"It is possible to spend only positive value of {nameof(this.GetType)}");

            if (HasEnough(valueToSpend) == false)
                throw new ArgumentException($"Not enough currency to spend");

            _currentValue -= valueToSpend;
            Spent?.Invoke(valueToSpend, _currentValue);
            ValueChanged?.Invoke(_currentValue);
        }

        public bool HasEnough(int valueToCompare) => valueToCompare <= _currentValue;
    }
}
