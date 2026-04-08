using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class dynamicMesh : MonoBehaviour
{
    [Min(1)] public int width = 10; 
    [Min(1)] public int height = 10;
    
    public Mesh GenerateGridMesh(Mesh target = null)
    {
        int vertCountX = width+1; 
        int VertCountZ = height +1;

        Vector3[] vertices = new Vector3[vertCountX*VertCountZ];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[width*height*6];

        //vertex index
        int vi=0;
        float offsetX = -width *0.5f; 
        float offsetZ = -height* 0.5f; 

        //loop through 
        for(int z=0; z<VertCountZ; z++)
        {
            for(int x=0; x<vertCountX; x++)
            {
                //get vertice and uv data 
                vertices[vi]= new Vector3(x+offsetX, 0f, z + offsetZ);
                uvs[vi]= new Vector2((float)x/width, (float)z/height);
                vi++;
            }
        }

        int ti=0;
        for(int z=0; z < height; z++)
        {
            for(int x=0; x<width; x++)
            {
                //getting data for each set of two triangles
                int v00 = z* vertCountX +x; 
                int v10 = v00+1; 
                int v01 = v00 +vertCountX;
                int v11 = v01+1; 

                triangles[ti++]=v00;
                triangles[ti++]=v01;
                triangles[ti++]=v10;

                triangles[ti++]=v10;
                triangles[ti++]=v01;
                triangles[ti++]=v11;
            }
        }

        Mesh m = target ?? new Mesh(); 
        m.Clear(); 
        m.vertices=vertices; 
        m.uv = uvs; 
        m.triangles = triangles; 
        //these methods smooth things out 
        m.RecalculateNormals(); 
        m.RecalculateBounds(); 
        return m; 
    } 

    //function: applymeshtofilter 
    public void ApplyToMeshFilter(Mesh toAssign)
    {
        if(toAssign==null) return; 
        MeshFilter mf = GetComponent<MeshFilter>();
        if(mf==null) mf=gameObject.AddComponent<MeshFilter>();
        mf.sharedMesh=toAssign;  
    }
}
