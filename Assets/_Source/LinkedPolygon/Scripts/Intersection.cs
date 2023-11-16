using UnityEngine;

namespace LinkedListPolygon
{
    public class Intersection
    {
        public readonly Vector2 Position;
        public readonly PolygonPoint AfterPoint;

        public Intersection(Vector2 position, PolygonPoint afterPoint)
        {
            Position = position;
            AfterPoint = afterPoint;
        }
    }
}