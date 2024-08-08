using UnityEngine;
using UnitySpriteCutter.Control;

namespace FurryCutter.Presentation.Visual
{
    [RequireComponent(typeof(LineRenderer))]
    public class MouseInputRepresentation : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private ILineInput _lineInput;
        private Vector2 _mouseStart;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void Init(ILineInput input)
        {
            _lineInput = input;
            _lineInput.Pressed += OnPressed;
        }

        private void OnPressed(Vector2 pressScreenPos)
        {
            _mouseStart = Camera.main.ScreenToWorldPoint(pressScreenPos);
        }

        private void Update()
        {
            if (_lineInput != null && _lineInput.IsPressed)
            {
                _lineRenderer.enabled = true;

                Vector2 mouseEnd = Camera.main.ScreenToWorldPoint(_lineInput.CurrentPointerPosition);

                _lineRenderer.SetPosition(0, _mouseStart);
                _lineRenderer.SetPosition(1, mouseEnd);
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }
    }
}
