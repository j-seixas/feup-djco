using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    public int numberToGive;

    int playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        //Only give USBs if the trigger collision is with the player 
        if (collision.gameObject.layer != playerLayer)
            return;

        // Gives USBs to Player
        collision.GetComponent<PlayerHealth>().GiveHealth(numberToGive);
        SoundManager.PlaySound("usb_collect");


        // Destroys Object
        Destroy(gameObject);
    }
}
