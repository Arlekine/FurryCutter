using UnityEngine;
using UnityEngine.UI;


namespace GenericUI
{
    public class SliderTimerView : TimerView
    {
        [SerializeField] private Slider _slider;

        protected override void UpdateView()
        {
            _slider.value = _timer.TimeLeftNormalized;
        }

        private void OnValidate()
        {
            _slider.minValue = 0;
            _slider.maxValue = 1;
        }
    }
}
