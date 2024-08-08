using System.Collections.Generic;
using UnityEngine;

namespace UnitySpriteCutter.Tools
{
    internal sealed class PolygonColliderParametersRepresentation
    {
        internal List<Vector2[]> Paths = new List<Vector2[]>();

        private Vector2 _offset;
        private bool _isTrigger;
        private PhysicsMaterial2D _sharedMaterial;
        private bool _usedByEffector;
        private bool _enabled;

        public void CopyParametersFrom(Collider2D origin)
        {
            _isTrigger = origin.isTrigger;
            _offset = origin.offset;
            _sharedMaterial = origin.sharedMaterial;
            _usedByEffector = origin.usedByEffector;
            _enabled = origin.enabled;
        }

        public void PasteTo(PolygonCollider2D polygonCollider)
        {
            polygonCollider.pathCount = Paths.Count;
            for (var i = 0; i < Paths.Count; i++) polygonCollider.SetPath(i, Paths[i]);

            polygonCollider.isTrigger = _isTrigger;
            polygonCollider.offset = _offset;
            polygonCollider.sharedMaterial = _sharedMaterial;
            polygonCollider.usedByEffector = _usedByEffector;
            polygonCollider.enabled = _enabled;
        }
    }
}