using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnitySpriteCutter.Cutters;

namespace UnitySpriteCutter.Testing
{
    public class TestingRoot : MonoBehaviour
    {
        [SerializeField] private PolygonCollider2D _polygon;

        [Space]
        [SerializeField] private Transform _lineStart;
        [SerializeField] private Transform _lineEnd;

        private List<List<Vector2>> _polygonsToDraw = new List<List<Vector2>>();

        [EditorButton]
        private void UpdatePolygon()
        {
            ClearPoints();
            _polygonsToDraw.Add(GetPolygon());
        }

        [EditorButton]
        private void Cut()
        {
            ClearPoints();
            var result = ShapeCutter.CutShape(_lineStart.position, _lineEnd.position, GetPolygon().ToArray());

            foreach (var shape in result)
            {
                _polygonsToDraw.Add(shape.ToList());

                var newCol = Instantiate(_polygon);

                var localShape = new Vector2[shape.Length];

                for (int i = 0; i < shape.Length; i++)
                {
                    localShape[i] = (_polygon.transform.InverseTransformPoint(shape[i]));
                }


                newCol.SetPath(0, localShape);
            }
        }

        [EditorButton]
        private void ClearPoints()
        {
            _polygonsToDraw.Clear();
        }

        private List<Vector2> GetPolygon()
        {
            var globalPoints = new List<Vector2>();

            for (int i = 0; i < _polygon.points.Length; i++)
            {
                globalPoints.Add(_polygon.transform.TransformPoint(_polygon.points[i]));
            }

            return globalPoints;
        }

        private void OnDrawGizmos()
        {
            if (_lineStart != null && _lineEnd != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(_lineStart.position, _lineEnd.position);
            }

            Gizmos.color = Color.gray;
            if (_polygonsToDraw.Count > 0)
            {
                foreach (var polygon in _polygonsToDraw)
                {
                    if (polygon.Count >= 3)
                    {
                        for (int i = 0; i < polygon.Count; i++)
                        {
                            Gizmos.DrawSphere(polygon[i], 0.2f);

                            if (i > 0)
                            {
                                Gizmos.DrawLine(polygon[i], polygon[i - 1]);
                            }
                            
                            if (i == polygon.Count - 1)
                            {
                                Gizmos.DrawLine(polygon[i], polygon[0]);
                            }
                        }
                    }
                }
            }
        }
    }
}