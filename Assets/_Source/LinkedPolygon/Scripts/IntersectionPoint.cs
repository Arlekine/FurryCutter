using UnityEngine;

namespace LinkedListPolygon
{
    public class IntersectionPoint : PolygonPoint
    {
        public IntersectionPoint ConnectedIntersectionPoint;

        public IntersectionPoint(Vector2 position) : base(position) { }
    }
}