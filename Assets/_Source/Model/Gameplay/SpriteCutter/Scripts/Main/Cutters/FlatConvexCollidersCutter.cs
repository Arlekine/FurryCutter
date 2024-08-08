using UnityEngine;
using System.Collections.Generic;
using UnitySpriteCutter.Tools;

namespace UnitySpriteCutter.Cutters
{
    internal static class FlatConvexCollidersCutter
    {
        public class CutResult
        {
            public List<PolygonColliderParametersRepresentation> Representations;

            public bool DidNotCut()
            {
                return Representations.Count != 0;
            }
        }

        public static CutResult Cut(Vector2 lineStart, Vector2 lineEnd, Collider2D[] colliders)
        {
            CutResult result = new CutResult();
            result.Representations = new List<PolygonColliderParametersRepresentation>();

            foreach (Collider2D collider in colliders)
            {
                List<Vector2[]> paths = ColliderPathsCreator.GetPolygonColliderPathsFrom(collider);
                foreach (Vector2[] path in paths)
                {
                    var cutResult = ShapeCutter.CutShape(lineStart, lineEnd, path);

                    foreach (var shape in cutResult)
                    {
                        if (shape.Length > 0)
                        {
                            PolygonColliderParametersRepresentation
                                repr = new PolygonColliderParametersRepresentation();
                            repr.CopyParametersFrom(collider);
                            repr.Paths.Add(shape);
                            result.Representations.Add(repr);
                        }
                    }
                }
            }

            return result;
        }
    }
}