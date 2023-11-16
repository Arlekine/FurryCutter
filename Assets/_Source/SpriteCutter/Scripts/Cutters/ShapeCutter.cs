using System;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkedListPolygon;

namespace UnitySpriteCutter.Cutters {
	
	internal static class ShapeCutter {

		public class CutResult {
			public Vector2[] firstSidePoints;
			public Vector2[] secondSidePoints;
		}

		private class InfiniteLine {
			
			public float a;
			public float b;

			public InfiniteLine( Vector2 segmentRepresentationStart, Vector2 segmentRepresentationEnd ) {
				Vector2 offset = segmentRepresentationEnd - segmentRepresentationStart;
				Vector2 sum = segmentRepresentationStart + segmentRepresentationEnd;
				if ( offset.y == 0 ) {
					a = 0;
					b = segmentRepresentationStart.y;
				} else {
					if ( offset.x == 0 ) {
						// It isn't a mathematical function - let's fake it!
						offset.x = 0.01f;
					}
					a = offset.y / offset.x;
					b = ( sum.y - ( a * sum.x ) ) / 2;
				}
			}
			
			public bool PointBelowLine( Vector2 point ) {
				return ( point.y < ( a * point.x + b )  );
			}
			
			public bool IntersectsWithSegment( Vector2 start, Vector2 end ) {
				bool firstPointUnder = PointBelowLine( start );
				bool secondPointUnder = PointBelowLine( end );
				return ( firstPointUnder != secondPointUnder );
			}

			public Vector2 IntersectionWithOtherLine( InfiniteLine other ) {
				Vector2 result = new Vector2();
				result.x = ( other.b - b ) / ( a - other.a );
				result.y = a * result.x + b;
				return result;
			}
		}

        private static void InsertIntersectionPointsInPolygon(Vector2 lineStart, Vector2 lineEnd, Polygon polygon)
        {
            var intersections = new List<Intersection>();
            InfiniteLine cuttingLine = new InfiniteLine(lineStart, lineEnd);

            foreach (var point in polygon)
            {
                if (cuttingLine.IntersectsWithSegment(point.Position, point.Next.Position))
                {
                    InfiniteLine lastTwoPointsLine = new InfiniteLine(point.Next.Position, point.Position);
                    Vector2 intersectionPoint = cuttingLine.IntersectionWithOtherLine(lastTwoPointsLine);
                    intersections.Add(new Intersection(intersectionPoint, point));

                }
            }

            //TODO: make sure that point is out of polygon
            intersections.Sort(new PointDistanceComparer(lineStart));
            if (intersections.Count % 2 != 0)
                throw new ArgumentException("Unexpected intersection behaviour - line intersects shape in not even amount of points");

            for (int i = 0; i < intersections.Count; i+=2)
			{
				var newPoint1 = new IntersectionPoint(intersections[i].Position);
				var newPoint2 = new IntersectionPoint(intersections[i+1].Position);

                newPoint1.ConnectedIntersectionPoint = newPoint2;
                newPoint2.ConnectedIntersectionPoint = newPoint1;

                polygon.InsertAfter(newPoint1, intersections[i].AfterPoint);
                polygon.InsertAfter(newPoint2, intersections[i+1].AfterPoint);
            }
        }

        public static List<Vector2[]> CutShape(Vector2 lineStart, Vector2 lineEnd, Vector2[] shape)
        {
            var polygon = new Polygon(shape);
            var remainingPoints = polygon.GetList();
			InfiniteLine cuttingLine = new InfiniteLine(lineStart, lineEnd);

			InsertIntersectionPointsInPolygon(lineStart, lineEnd, polygon);

            var finalShapes = new List<Vector2[]>();

            while (remainingPoints.Count > 0)
            {
                var newShape = new List<Vector2>();

                var startPoint = remainingPoints[0];
                var currentPoint = startPoint;
                var prevPoint = startPoint;
                var directionIsNext = true;

                while (currentPoint.Next != startPoint)
                {
                    newShape.Add(currentPoint.Position);
                    remainingPoints.Remove(currentPoint);
                    var pointBuffer = currentPoint;

					if (currentPoint is IntersectionPoint intersectionPoint)
                    {
                        if (prevPoint is IntersectionPoint == false)
                        {
                            currentPoint = intersectionPoint.ConnectedIntersectionPoint;
                        }
                        else
                        {
                            if (cuttingLine.PointBelowLine(currentPoint.Next.Position) == cuttingLine.PointBelowLine(startPoint.Position))
							{
								currentPoint = currentPoint.Next;
                                directionIsNext = currentPoint.Next != pointBuffer;
                            }
                            else
                            {
                                currentPoint = currentPoint.Previous;
                                directionIsNext = currentPoint.Next != pointBuffer;
							}
                        }
                    }
                    else
                    {
                        currentPoint = directionIsNext ? currentPoint.Next : currentPoint.Previous;
                    }

					prevPoint = pointBuffer;
				}

				newShape.Add(currentPoint.Position);
                remainingPoints.Remove(currentPoint);
				finalShapes.Add(newShape.ToArray());
            }

            return finalShapes;
        }

        public static CutResult CutShapeIntoTwo( Vector2 lineStart, Vector2 lineEnd, Vector2[] shape ) 
        {
			List<Vector2> firstSide = new List<Vector2>();
			List<Vector2> secondSide = new List<Vector2>();

			InfiniteLine cuttingLine = new InfiniteLine( lineStart, lineEnd );

			int intersectionsFound = 0;

			for ( int i = 0; i < shape.Length; i++ ) {
				Vector2 point = shape[ i ];

				Vector2 previousPoint;
				if ( i == 0 ) {
					previousPoint = shape[ shape.Length - 1 ];
				} else {
					previousPoint = shape[ i - 1 ];
				}

				if ( cuttingLine.IntersectsWithSegment( previousPoint, point ) ) 
                {
					InfiniteLine lastTwoPointsLine = new InfiniteLine( previousPoint, point );
					Vector2 intersectionPoint = cuttingLine.IntersectionWithOtherLine( lastTwoPointsLine );
					firstSide.Add( intersectionPoint );
					secondSide.Add( intersectionPoint );
					intersectionsFound++;
				}

				if ( cuttingLine.PointBelowLine( point ) ) {
					firstSide.Add( point );
				} else {
					secondSide.Add( point );
				}
			}

			if ( intersectionsFound > 2 )
				throw new System.Exception( "SpriteCutter cannot cut through non-convex shapes! Adjust your colliders shapes to be convex!" );
			
			CutResult result = new CutResult();
			result.firstSidePoints = firstSide.ToArray();
			result.secondSidePoints = secondSide.ToArray();
			return result;
		}
    }
	
}