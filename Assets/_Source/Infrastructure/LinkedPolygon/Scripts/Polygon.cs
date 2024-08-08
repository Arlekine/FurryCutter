using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinkedListPolygon
{
    public sealed class Polygon : IEnumerable<PolygonPoint>
    {
        public readonly PolygonPoint Start;

        public Polygon(Vector2[] shape)
        {
            if (shape.Length < 3)
                throw new ArgumentException("Can't create polygon with less than 3 points");

            Start = new PolygonPoint(shape[0]);
            var prevPoint = Start;

            for (int i = 1; i < shape.Length; i++)
            {
                var newPoint = new PolygonPoint(shape[i]);
                prevPoint.Next = newPoint;
                newPoint.Previous = prevPoint;

                if (i == shape.Length - 1)
                {
                    newPoint.Next = Start;
                    Start.Previous = newPoint;
                }

                prevPoint = newPoint;
            }
        }

        public List<PolygonPoint> GetList()
        {
            var list = new List<PolygonPoint>();
            foreach (var point in this)
            {
                list.Add(point);
            }

            return list;
        }

        public void InsertAfter(PolygonPoint pointToInsert, PolygonPoint after)
        {
            var prevNext = after.Next;

            after.Next = pointToInsert;
            pointToInsert.Previous = after;
            pointToInsert.Next = prevNext;

            prevNext.Previous = pointToInsert;
        }

        public IEnumerator<PolygonPoint> GetEnumerator() => new PolygonPointEnumerator(Start);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}