using System;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

namespace GenericUI
{
    public class UIToSceneDragger : MonoBehaviour
    {
        [SerializeField] private float _zPos;

        private IEnumerable<ILeanSelectable> _dragButtons;

        private LeanFinger _currentDragFinger;
        private ILeanSelectable _selectedElement;

        public event Action<ILeanSelectable, Vector3> DragStared;
        public event Action<ILeanSelectable, Vector3> Dragging;
        public event Action<ILeanSelectable, Vector3> DragFinished;

        public void SetElements(IEnumerable<ILeanSelectable> elements)
        {
            ClearButtons();
            _dragButtons = elements;

            foreach (var dragButton in _dragButtons)
            {
                dragButton.Selected += OnElementSelected;
            }
        }

        private void OnEnable()
        {
            LeanTouch.OnFingerUp += OnFingerUp;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerUp -= OnFingerUp;
        }

        private void OnDestroy()
        {
            ClearButtons();
        }

        private void ClearButtons()
        {
            if (_dragButtons != null)
            {
                foreach (var dragButton in _dragButtons)
                {
                    dragButton.Selected -= OnElementSelected;
                }

                _dragButtons = null;
            }
        }

        private void OnFingerUp(LeanFinger finger)
        {
            if (_currentDragFinger == finger)
            {
                DragFinished?.Invoke(_selectedElement, FingerToScenePosition(_currentDragFinger));

                _selectedElement = null;
                _currentDragFinger = null;
            }
        }

        private void OnElementSelected(LeanFinger finger, ILeanSelectable selectable)
        {
            _currentDragFinger = finger;
            _selectedElement = selectable;
            DragStared?.Invoke(selectable, FingerToScenePosition(_currentDragFinger));
        }

        private void Update()
        {
            if (_currentDragFinger != null)
            {
                var scenePosition = FingerToScenePosition(_currentDragFinger);
                scenePosition.z = _zPos;

                Dragging?.Invoke(_selectedElement, scenePosition);
            }
        }

        private Vector3 FingerToScenePosition(LeanFinger finger) => Camera.main.ScreenToWorldPoint(new Vector3(_currentDragFinger.ScreenPosition.x, _currentDragFinger.ScreenPosition.y, -Camera.main.transform.position.z));
    }
}
