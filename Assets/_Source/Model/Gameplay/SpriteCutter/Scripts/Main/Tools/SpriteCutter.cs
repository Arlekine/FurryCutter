using System.Collections.Generic;
using UnityEngine;
using UnitySpriteCutter.Cutters;
using UnitySpriteCutter.Tools;
using Object = UnityEngine.Object;

namespace UnitySpriteCutter
{
    internal sealed class SpriteCutterInput
    {
        public SpriteCutterInput(Vector2 lineStart, Vector2 lineEnd, Cuttable cuttable)
        {
            LineStart = lineStart;
            LineEnd = lineEnd;
            Cuttable = cuttable;
            GameObject = cuttable.gameObject;
        }

        public Vector2 LineStart { get; }

        public Vector2 LineEnd { get; }

        public Cuttable Cuttable { get; }

        public GameObject GameObject { get; }
    }

    internal sealed class SpriteCutterOutput
    {
        public readonly Cuttable oldCuttable;
        public readonly List<Cuttable> NewCuttables = new List<Cuttable>();

        public SpriteCutterOutput(Cuttable oldCuttable)
        {
            this.oldCuttable = oldCuttable;
        }
    }

    internal static class SpriteCutter
    {
        public static SpriteCutterOutput Cut(SpriteCutterInput input)
        {
            if (input.Cuttable == null)
            {
                Debug.LogError("SpriteCutter.Cut excecuted with null cuttable!");
                return null;
            }

            var oldCuttable = input.Cuttable;

            var localLineStart = input.GameObject.transform.InverseTransformPoint(input.LineStart);
            var localLineEnd = input.GameObject.transform.InverseTransformPoint(input.LineEnd);

            var spriteRenderer = input.GameObject.GetComponent<SpriteRenderer>();
            var meshRenderer = input.GameObject.GetComponent<MeshRenderer>();

            var collidersCutResults = input.GameObject.GetComponent<PolygonCollider2D>();

            var output = new SpriteCutterOutput(oldCuttable);

            //get shapes

            var originMesh = GetOriginMeshFrom(spriteRenderer, meshRenderer);
            Vector2[] shape = MeshCreator.ConvertVerticesToShape(originMesh.vertices);
            var resultShapes = ShapeCutter.CutShape(localLineStart, localLineEnd, collidersCutResults.points);

            //create new meshes from it 

            for (int i = 0; i < resultShapes.Count; i++)
            {
                var colliderRepresentation = new PolygonColliderParametersRepresentation();
                colliderRepresentation.CopyParametersFrom(collidersCutResults);
                colliderRepresentation.Paths.Add(resultShapes[i]);

                var mesh = MeshCreator.CreateMeshFromShape(originMesh, resultShapes[i]);
                var newGO = SpriteCutterGameObject.CreateAsCopyOf(input.GameObject, i.ToString());
                PrepareResultGameObject(newGO, spriteRenderer, meshRenderer, mesh, colliderRepresentation);

                output.NewCuttables.Add(newGO.gameObject.AddComponent<Cuttable>());
            }

            Object.Destroy(input.GameObject);

            return output;
        }

        private static Mesh GetOriginMeshFrom(SpriteRenderer spriteRenderer, MeshRenderer meshRenderer)
        {
            if (spriteRenderer != null)
                return SpriteMeshConstructor.ConstructFromRendererBounds(spriteRenderer);
            return meshRenderer.GetComponent<MeshFilter>().mesh;
        }

        private static void PrepareResultGameObject(SpriteCutterGameObject resultGameObject, SpriteRenderer spriteRenderer, MeshRenderer meshRenderer, Mesh mesh, PolygonColliderParametersRepresentation colliderRepresentations)
        {
            resultGameObject.AssignMeshFilter(mesh);
            if (spriteRenderer != null)
                resultGameObject.AssignMeshRendererFrom(spriteRenderer);
            else
                resultGameObject.AssignMeshRendererFrom(meshRenderer);

            if (colliderRepresentations != null) resultGameObject.BuildCollidersFrom(colliderRepresentations);
        }

        private static void PrepareResultGameObject(SpriteCutterGameObject resultGameObject, RendererParametersRepresentation tempParameters, Mesh mesh, PolygonColliderParametersRepresentation colliderRepresentations)
        {
            resultGameObject.AssignMeshFilter(mesh);
            resultGameObject.AssignMeshRendererFrom(tempParameters);

            if (colliderRepresentations != null) resultGameObject.BuildCollidersFrom(colliderRepresentations);
        }
    }
}