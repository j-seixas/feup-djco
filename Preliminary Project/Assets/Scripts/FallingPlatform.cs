// This script handles detecting collisions with traps and telling the Game Manager
// when the player dies

using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    int playerLayer;
    int platformsLayer;
    bool hasFell;
    float previousGravity;
    public float gravityScale = 0.2f;
    public bool hasFinishedFalling;
    Rigidbody2D rigidBody;
    Collider2D triggerBox;

    void Start() {
        hasFell = false;
        hasFinishedFalling = false;
        playerLayer = LayerMask.NameToLayer("Player");
        platformsLayer = LayerMask.NameToLayer("Platforms");
        rigidBody = GetComponent<Rigidbody2D>();
        triggerBox = GetComponent<Collider2D>();

        previousGravity = rigidBody.gravityScale;
        rigidBody.isKinematic = true; 
        rigidBody.gravityScale = 0f;
        rigidBody.mass = 100f;
    }

	void OnTriggerEnter2D(Collider2D collision)
	{
		//Only start falling if the trigger collision is with the player 
		if (collision.gameObject.layer != playerLayer)
			return;

        //Disable subsequent collisions
        if(hasFell)
            return;

        //Disable trigger box
        triggerBox.enabled = false;
        
        //Let it fall
        hasFell = true;
        rigidBody.isKinematic = false;
        rigidBody.gravityScale = previousGravity * gravityScale;

        //Debug.Log("Platform falling");
	}

    void OnCollisionEnter2D(Collision2D collision) 
    {
        //When a platform is hit, it has finished falling
        if(platformsLayer == collision.gameObject.layer){
            hasFinishedFalling = true;
            SoundManager.PlaySound("traps_hit");
        }
    }
}
