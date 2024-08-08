using UnityEngine;

namespace LinkedListPolygon
{
    public class PolygonPoint
    {
        public readonly Vector2 Position;
        public PolygonPoint Previous;
        public PolygonPoint Next;

        public PolygonPoint(Vector2 position)
        {
            Position = position;
        }
    }
}