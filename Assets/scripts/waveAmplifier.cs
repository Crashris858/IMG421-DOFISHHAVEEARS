using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class waveAmplifier : MonoBehaviour
{
    private MeshFilter meshFilter; 
    private List<wave> waves; 

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>(); 
        //dont call this every frame
        waves= FindObjectsOfType<wave>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyWaves();
    }
    
    void ApplyWaves()
    {
        var mesh = meshFilter.mesh; 
        var vertices = mesh.vertices; 

        for(int i=0; i<vertices.Length; i++)
        {
            Vector3 world = transform.TransformPoint(vertices[i]);
            float height =0; 
            foreach(var wave in waves)
            {
                height = wave.GetHeight(world.x, world.z);
                Vector3 newWorld = new Vector3(world.x, height, world.z);
                vertices[i]= transform.InverseTransformPoint(newWorld);
            }
        }

        mesh.vertices = vertices; 
        mesh.RecalculateNormals(); 
    }
}
