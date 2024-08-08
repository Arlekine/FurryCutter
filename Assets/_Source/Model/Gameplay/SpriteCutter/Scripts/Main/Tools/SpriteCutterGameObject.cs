using UnityEngine;
using UnitySpriteCutter.Tools;

namespace UnitySpriteCutter
{
    internal class SpriteCutterGameObject
    {
        private SpriteCutterGameObject()
        {}

        public GameObject gameObject { get; private set; }

        public static SpriteCutterGameObject CreateAs(GameObject origin)
        {
            var result = new SpriteCutterGameObject();
            result.gameObject = origin;
            return result;
        }

        public static SpriteCutterGameObject CreateAsCopyOf(GameObject origin, string additionalName)
        {
            var result = new SpriteCutterGameObject();
            result.gameObject = new GameObject(origin.name + "_" + additionalName);
            result.gameObject.transform.parent = origin.transform.parent;
            result.CopyGameObjectParametersFrom(origin);
            result.CopyTransformFrom(origin.transform);

            var rigidbody = origin.GetComponent<Rigidbody2D>();

            if (rigidbody != null)
            {
                var newRigidbody = result.gameObject.AddComponent<Rigidbody2D>();
                newRigidbody.bodyType = rigidbody.bodyType;
                newRigidbody.mass = rigidbody.mass;
            }

            return result;
        }

        private void CopyGameObjectParametersFrom(GameObject other)
        {
            gameObject.isStatic = other.isStatic;
            gameObject.layer = other.layer;
            gameObject.tag = other.tag;
        }

        private void CopyTransformFrom(Transform transform)
        {
            gameObject.transform.position = transform.position;
            gameObject.transform.rotation = transform.rotation;
            gameObject.transform.localScale = transform.localScale;
        }

        public void AssignMeshFilter(Mesh mesh)
        {
            var meshFilter = gameObject.GetComponent<MeshFilter>();

            if (meshFilter == null)
                meshFilter = gameObject.AddComponent<MeshFilter>();

            meshFilter.mesh = mesh;
        }

        public void AssignMeshRendererFrom(SpriteRenderer spriteRenderer)
        {
            var tempParameters = new RendererParametersRepresentation();
            tempParameters.CopyFrom(spriteRenderer);
            AssignMeshRendererFrom(tempParameters);
        }

        public void AssignMeshRendererFrom(MeshRenderer meshRenderer)
        {
            var tempParameters = new RendererParametersRepresentation();
            tempParameters.CopyFrom(meshRenderer);
            AssignMeshRendererFrom(tempParameters);
        }

        public void AssignMeshRendererFrom(RendererParametersRepresentation tempParameters)
        {
            var meshRenderer = gameObject.GetComponent<MeshRenderer>();

            if (meshRenderer == null)
                meshRenderer = gameObject.AddComponent<MeshRenderer>();

            tempParameters.PasteTo(meshRenderer);
        }

        public void BuildCollidersFrom(PolygonColliderParametersRepresentation representation)
        {
            foreach (var collider in gameObject.GetComponents<Collider2D>()) Object.Destroy(collider);

            var newCollider = gameObject.AddComponent<PolygonCollider2D>();
            representation.PasteTo(newCollider);
        }
    }
}