using System.Collections;
using TMPro;
using UnityEngine;

namespace GenericUI
{
    public class PopUpText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _delayBeforeDisappear = 1f;

        public RectTransform RectTransform => _rectTransform;

        public void Show(string text)
        {
            _text.text = text;

            this.ActionAfterPause(_delayBeforeDisappear, () =>
            {
                Destroy(gameObject, 0.3f);
            });
        }

        private void OnValidate()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
    }
}
