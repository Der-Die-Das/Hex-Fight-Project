using UnityEngine;
using System.Collections;
using UnityEditor;

namespace HexFight
{
    [CustomEditor(typeof(Map))]
    [CanEditMultipleObjects]
    public class MapEditor : Editor
    {
        SerializedProperty widthProp;
        SerializedProperty heightProp;
        SerializedProperty hexPrefabProp;

        void OnEnable()
        {
            widthProp = serializedObject.FindProperty("width");
            heightProp = serializedObject.FindProperty("height");
            hexPrefabProp = serializedObject.FindProperty("hexPrefab");
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            Map myscript = (Map)target;
            if (GUILayout.Button("Generate Map"))
            {
                myscript.ReCreateMap();
            }
        }
    }
}