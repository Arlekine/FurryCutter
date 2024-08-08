using System;
using Lean.Touch;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GenericUI
{
    public class SelectableIconWithText : MonoBehaviour, ILeanSelectable
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _quantityText;
        [SerializeField] private LeanSelectable _selectable;

        public event Action<LeanFinger, ILeanSelectable> Selected;

        public void SetIcon(Sprite icon) => _icon.sprite = icon;
        public void SetText(string text) => _quantityText.text = text;

        private void OnEnable()
        {
            _selectable.OnSelect.AddListener(OnSelected);
        }

        private void OnDisable()
        {
            _selectable.OnSelect.RemoveListener(OnSelected);
        }

        private void OnSelected(LeanFinger finger)
        {
            Selected?.Invoke(finger, this);
        }
    }
}
