// This script handles detecting collisions with traps and telling the Game Manager
// when the player dies

using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    int playerLayer;
    bool hasFell;
    float previousGravity;
    public float gravityScale = 0.2f;
    Rigidbody2D rigidBody;
    Collider2D triggerBox;

    void Start() {
        hasFell = false;
        playerLayer = LayerMask.NameToLayer("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        triggerBox = GetComponent<Collider2D>();

        previousGravity = rigidBody.gravityScale;
        rigidBody.isKinematic = true; 
        rigidBody.gravityScale = 0f;
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
}
