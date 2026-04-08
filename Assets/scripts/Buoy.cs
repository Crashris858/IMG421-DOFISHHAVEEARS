using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Buoy : MonoBehaviour
{
    public Rigidbody rigidbody; 
    public float buoyancy=1f; 
    public float CurrentWaterHeight =0f; 
    private List<wave> waves; 

    // Start is called before the first frame update
    void Start()
    {
        waves=FindObjectsOfType<wave>().ToList(); 
    }

    void FixedUpdate()
    {
        CurrentWaterHeight=0f; 
        foreach(var wave in waves)
        {
            CurrentWaterHeight+=wave.GetHeight(transform.position.x,transform.position.z);
            if(transform.position.y<CurrentWaterHeight)
            {
                float submersion = CurrentWaterHeight-transform.position.y; 
                float force = rigidbody.mass*Physics.gravity.magnitude*buoyancy*submersion; 
                //rigidbody.AddForce(Vector3.up*force);
                rigidbody.AddForceAtPosition(Vector3.up*force,transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
