using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFire : MonoBehaviour
{

    public GameObject projectile;
    private float speed = 20.0f;
    private float activateRate = 2.0f;
    private float nextActivationTime;
    private Transform target;
    private bool passed;
    void Start()
    {
        target = GameObject.Find("PlayerShip").transform;
        nextActivationTime = 0.0f;
    }

    void Update()
    {

        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < 5)
                passed = true;

        }


        // if the Fire1 button (default is left ctrl) is pressed and the alloted time has passed
        if (target != null)
        {
            if (Time.time > nextActivationTime && !passed)
            {
                nextActivationTime = Time.time + activateRate; // reset the timer
                Activate(); // do whatever the fire button controls
            }
        }



    }

    void Activate()
    {
        // create a clone of the projectile at the location & orientation of the script's parent     
        if (target != null)
        {
            GameObject clone = Instantiate(projectile, transform.position, transform.rotation);
            clone.GetComponent<Renderer>().material.color = Color.red;
            // add some force to send the projectile off in its forward direction
            clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        }

        // ignore the collider on the object the script is on
        //Physics.IgnoreCollision(clone.collider, transform.collider);


    }

}
