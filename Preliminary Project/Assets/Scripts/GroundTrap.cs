using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrap : MonoBehaviour
{
    int playerLayer;
    public float timeToDestroy;
    bool toDestroy;

    void Start()
    {
        toDestroy = false;
        playerLayer = LayerMask.NameToLayer("Player");
    }

    void Update()
    {

        //If player collided with it
        if (toDestroy) {

            //Check time between first contact of the player and the time to destroy the platform
            if(timeToDestroy > 0)
                timeToDestroy -= Time.deltaTime;
            else
                Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       
        //Only destroy if the collision is with the player 
        if (collision.gameObject.layer != playerLayer)
            return;
        
        //Disable subsequent collisions
        if (toDestroy)
            return;

        //Enable destruction
        toDestroy = true;
    }


}