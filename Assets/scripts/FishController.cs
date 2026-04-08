
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class FishController : MonoBehaviour
{
    public Rigidbody rigidbody; 
    public scoreboard ScoreBoard; 
    public float thrustForce = 10f; 
    public float turnTorque = 10f; 
    public float maxSpeed = 12f;  
    public Buoy GrounDetector; 
    public int BuoyNum=5; 
    private float PrevH; 
    private float RotationX, RotationY =0; 
    bool StartTrick=false;  

    // Start is called before the first frame update
    void Awake()
    {
        if(!rigidbody) rigidbody = GetComponent<Rigidbody>(); 
        StartTrick=false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //always aviable mechanics 
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //add downward force belwo waves
            rigidbody.AddForce(Vector3.down*(thrustForce/2), ForceMode.Impulse);
            //increase buoyancy 
            
        }  
        //air state 
        if(GrounDetector.CurrentWaterHeight+2f<transform.position.y)
        {
            print("isAir");
            if(!StartTrick)
            {
                PrevH = Input.GetAxis("Horizontal");
                StartTrick=true; 
                //reduce buoyancy 
                ChangeBuyoancy(5.0f);
            }
            //ground movement 
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            //continue moving forward in direction 
            rigidbody.AddForce(-transform.forward*(PrevH/2)*thrustForce);

            //change controls to turn
            rigidbody.AddTorque(-transform.forward*v*thrustForce);
            rigidbody.AddTorque(Vector3.up*h*turnTorque);

            RotationX+=Mathf.Abs(v);
            RotationY+=Mathf.Abs(h);

            //award score if moving
            if(Mathf.Abs(v)>0||Mathf.Abs(h)>0)
            {
                ScoreBoard.Score+=1;   
            }
        }
        //ground state 
        else
        {
            //check on ground 
            
            //end trick logic
            if(StartTrick)
            {
                print(RotationX + RotationY);
                StartTrick=false;
                //check spins 
                int spins = (int)RotationX/40; 
                spins += (int)RotationY/20;
                spins*=50; 
                ScoreBoard.Score+=spins;  
                //update scoreboard
                ScoreBoard.UpdateCoolness();
                //reset Rotation
                RotationX=0f;
                RotationY=0f; 
                ChangeBuyoancy(2.0f);
            }
            //normal movement 
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            rigidbody.AddForce(-transform.forward*v*thrustForce);
            rigidbody.AddTorque(Vector3.up*h*turnTorque);
        }
    }

    public void ChangeBuyoancy(float Cbuoyancy)
    {
        //get list 
        GameObject[] BuoyList = GameObject.FindGameObjectsWithTag("buoy");
        foreach(GameObject CurrentB in BuoyList)
        {
            Buoy CurrentBuoy=CurrentB.GetComponent<Buoy>();
            if(CurrentBuoy is Buoy)
            {
                CurrentBuoy.buoyancy=Cbuoyancy; 
            } 
        }
    }
}
