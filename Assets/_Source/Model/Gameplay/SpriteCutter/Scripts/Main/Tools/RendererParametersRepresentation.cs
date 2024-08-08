using UnityEngine;

namespace UnitySpriteCutter.Tools
{
    internal class RendererParametersRepresentation
    {
        private Material[] sharedMaterials;
        private Material[] materials;
        private int sortingOrder;
        private int sortingLayerID;
        private HideFlags hideFlags;
        private bool enabled;

        private Sprite sprite;
        private Color color;
        private Texture2D texture;

        internal void CopyFrom(SpriteRenderer from)
        {
            sharedMaterials = from.sharedMaterials;
            materials = from.materials;
            sortingOrder = from.sortingOrder;
            sortingLayerID = from.sortingLayerID;
            hideFlags = from.hideFlags;
            enabled = from.enabled;
            sprite = from.sprite;
            texture = from.sprite.texture;
            color = from.color;
        }

        internal void CopyFrom(MeshRenderer from)
        {
            sharedMaterials = from.sharedMaterials;
            materials = from.materials;
            sortingOrder = from.sortingOrder;
            sortingLayerID = from.sortingLayerID;
            hideFlags = from.hideFlags;
            enabled = from.enabled;
            sprite = null;
            color = from.material.color;
            texture = from.material.GetTexture("_MainTex") as Texture2D;
        }

        internal void PasteTo(SpriteRenderer to)
        {
            to.sharedMaterials = sharedMaterials;
            to.materials = materials;
            to.sortingOrder = sortingOrder;
            to.sortingLayerID = sortingLayerID;
            to.hideFlags = hideFlags;
            to.sprite = sprite;
        }

        internal void PasteTo(MeshRenderer to)
        {
            to.sharedMaterials = sharedMaterials;
            to.materials = materials;
            to.sortingOrder = sortingOrder;
            to.sortingLayerID = sortingLayerID;
            to.hideFlags = hideFlags;
            to.material.SetTexture("_MainTex", texture);
            to.enabled = enabled;
            to.material.color = color;
        }
    }
}