using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrap : MonoBehaviour
{
    int playerLayer;
    int platformsLayer;
    public float timeToFell;
    public float fellTime;
    bool deleteSides;
    bool hasFell;
    float previousGravity;
    public float gravityScale = 0.2f;
    Rigidbody2D rigidBody;
    Collider2D triggerBox;

    void Start()
    {
        hasFell = false;
        deleteSides = true;
        playerLayer = LayerMask.NameToLayer("Player");
        rigidBody = GetComponent<Rigidbody2D>();

        previousGravity = rigidBody.gravityScale;
        rigidBody.isKinematic = true;
        rigidBody.gravityScale = 0f;
        rigidBody.mass = 100f;
    }

    void Update()
    {

        Debug.Log(Time.deltaTime);
        if (hasFell) {
            if(timeToFell > 0)
                timeToFell -= Time.deltaTime;
            else
            {
                if(deleteSides)
                {
                    deleteSides = false;
                    //Remove sides
                    Destroy(GetComponent<Transform>().GetChild(2).gameObject);
                    Destroy(GetComponent<Transform>().GetChild(2).gameObject);

                    rigidBody.isKinematic = false;

                    rigidBody.gravityScale = previousGravity * gravityScale;

                    Debug.Log("Platform falling");
                }

                if (fellTime > 0)
                    fellTime -= Time.deltaTime;
                else
                    Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1  Platform falling");
        //Only start falling if the trigger collision is with the player 
        if (collision.gameObject.layer != playerLayer)
            return;
        Debug.Log("222 Platform falling");
        //Disable subsequent collisions
        if (hasFell)
            return;
        Debug.Log("33 Platform falling");
        

        //Let it fall
        hasFell = true;
    }


}