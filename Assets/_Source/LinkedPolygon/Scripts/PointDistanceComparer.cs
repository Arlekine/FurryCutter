using System.Collections.Generic;
using UnityEngine;

namespace LinkedListPolygon
{
    public class PointDistanceComparer : IComparer<Intersection>
    {
        private Vector2 _pointToComparer;

        public PointDistanceComparer(Vector2 pointToComparer)
        {
            _pointToComparer = pointToComparer;
        }

        public int Compare(Intersection a, Intersection b)
        {
            var aDistance = Vector2.Distance(_pointToComparer, a.Position);
            var bDistance = Vector2.Distance(_pointToComparer, b.Position);

            return aDistance.CompareTo(bDistance);
        }
    }
}