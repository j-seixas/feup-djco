// This script handles detecting collisions with traps and telling the Game Manager
// when the player dies

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	bool isAlive = true;				//Stores the player's "alive" state
	int trapsLayer;						//The layer the traps are on


	void Start()
	{
		//Get the integer representation of the "Traps" layer
		trapsLayer = LayerMask.NameToLayer("Traps");
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		//If the collided object isn't on the Traps layer OR if the player isn't currently
		//alive, exit. This is more efficient than string comparisons using Tags
		if (collision.gameObject.layer != trapsLayer || !isAlive)
			return;

		//Trap was hit, so set the player's alive state to false
		isAlive = false;

		//Disable player game object
		gameObject.SetActive(false);

		//Tell the Game Manager that the player died and tell the Audio Manager to play
		//the death audio
		//GameManager.PlayerDied();
		//AudioManager.PlayDeathAudio();
	}
}
