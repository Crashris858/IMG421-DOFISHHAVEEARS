
using Unity.VisualScripting;
using UnityEngine;

public class BoatFollowCamera : MonoBehaviour
{
    public Transform target;
    public Transform goalPosition; 
    public GameObject Fish; 
    public float smoothSpeed = 1;
    public float yLimit =0; 

    void LateUpdate()
    {
        Vector3 delta = goalPosition.position - transform.position;
        float distanceMultiplier = delta.x * delta.x + delta.y * delta.y + delta.z * delta.z;
        transform.position = Vector3.MoveTowards(transform.position, goalPosition.position, smoothSpeed * distanceMultiplier);
        Vector3 pos =transform.position;
        if(transform.position.y<Fish.transform.position.y)
        {
            pos.y=Fish.transform.position.y;
        }
        transform.position=pos;
        transform.LookAt(target);
    }
}
