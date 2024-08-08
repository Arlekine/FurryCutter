using UnityEngine;

namespace GenericUI
{
    public class CountDownPopUpTimer : TimerView
    {
        [SerializeField] private PopUpText _numberPrefab;
        [SerializeField] private RectTransform _numbersParent;

        private int _prevSecond = int.MaxValue;

        protected override void UpdateView()
        {
            if (_timer.TimeLeft.Seconds < _prevSecond)
            {
                _prevSecond = _timer.TimeLeft.Seconds;
                CreateSecondNumber(_timer.TimeLeft.Seconds + 1);
            }
        }

        private void CreateSecondNumber(int number)
        {
            var newPopUp = Instantiate(_numberPrefab, _numbersParent);
            newPopUp.Show(number.ToString());
        }
    }
}