using UnityEngine;
using System.Collections;
using UnityEditor;

namespace HexFight
{
    [CustomEditor(typeof(HexCell))]
    [CanEditMultipleObjects]
    public class HexCellEditor : Editor
    {
        SerializedProperty objectProp;
        SerializedProperty posProp;


        void OnEnable()
        {
            objectProp = serializedObject.FindProperty("placedObjectPrefab");
            posProp = serializedObject.FindProperty("position");
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            HexCell myscript = (HexCell)target;
            if (GUILayout.Button("Place Object"))
            {
                myscript.PlacePrefab();
            }
            if (GUILayout.Button("Destroy Object"))
            {
                myscript.DestroyObject();
            }
        }
    }
}