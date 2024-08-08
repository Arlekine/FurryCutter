using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FlatMeshCreation
{
    [CustomEditor(typeof(MeshFromPolygonColliderCreator))]
    public class MeshFromPolygonColliderCreatorEditor : Editor
    {
        private int _index;
        private List<string> _names = new List<string>();

        private SerializedProperty _polygonCollider;
        private SerializedProperty _materials;
        private SerializedProperty _sortingLayerName;
        private SerializedProperty _sortingLayer;

        public void OnEnable()
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layersProp = tagManager.FindProperty("m_SortingLayers");

            _polygonCollider = serializedObject.FindProperty("_polygonCollider");
            _materials = serializedObject.FindProperty("_materials");
            _sortingLayerName = serializedObject.FindProperty("_sortingLayerName");
            _sortingLayer = serializedObject.FindProperty("_sortingLayer");

            _names = new List<string>();

            for (int i = 0; i < layersProp.arraySize; i++)
            {
                SerializedProperty t = layersProp.GetArrayElementAtIndex(i);
                var name = t.FindPropertyRelative("name").stringValue;

                if (name == _sortingLayerName.stringValue)
                    _index = i;

                _names.Add(name);
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_polygonCollider, new GUIContent("Polygon Collider"));
            EditorGUILayout.PropertyField(_materials, new GUIContent("Materials"));
            EditorGUILayout.Space();

            _index = EditorGUILayout.Popup(new GUIContent("Sorting Layer"), _index, _names.ToArray(), GUILayout.ExpandWidth (true));

            _sortingLayerName.stringValue = _names[_index];

            EditorGUILayout.PropertyField(_sortingLayer, new GUIContent("Layer Index"));
            EditorGUILayout.Space();

            if (GUILayout.Button("Create Mesh"))
                ((MeshFromPolygonColliderCreator)target).CreateMeshOnThisObject();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}