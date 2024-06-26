using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLogBehaviour : MonoBehaviour
{

    public Transform[] waypoints;
    public float logSpeed = 5f;


    void FixedUpdate()
    {
        if (transform.position != waypoints[0].position) 
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[0].position, logSpeed * Time.deltaTime);
            
        }
        else
        {
            transform.position = waypoints[1].position;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);

           
            float centerZ = transform.position.z;
            
            
            Vector3 playerPosition = collision.transform.position;
            playerPosition.z = centerZ;
            collision.transform.position = playerPosition;

           
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}