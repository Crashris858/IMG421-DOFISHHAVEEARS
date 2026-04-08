using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(dynamicMesh))]
public class DynamicMeshEditor : Editor
{
    //
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 
        var dynamicMesh = (dynamicMesh)target;
        if(GUILayout.Button("Create Mesh"))
        {
            Debug.Log("MESHMEMESHME");
            var mesh = dynamicMesh.GenerateGridMesh(); 
            dynamicMesh.ApplyToMeshFilter(mesh);
        }
    }
}
