// This script controls the orb collectables. It is responsible for detecting collision
// with the player and reporting it to the game manager. Additionally, since the orb
// is a part of the level it will need to register itself with the game manager

using UnityEngine;

public class Orb : MonoBehaviour
{
	public GameObject explosionVFXPrefab;	//The visual effects for orb collection

	int playerLayer;						//The layer the player game object is on


	void Start()
	{
		//Get the integer representation of the "Player" layer
		playerLayer = LayerMask.NameToLayer("Player");

		//Register this orb with the game manager
		GameManager.RegisterOrb(this);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		//If the collided object isn't on the Player layer, exit. This is more 
		//efficient than string comparisons using Tags
		if (collision.gameObject.layer != playerLayer)
			return;

		//The orb has been touched by the Player, so instantiate an explosion prefab
		//at this location and rotation
		Instantiate(explosionVFXPrefab, transform.position, transform.rotation);
		
		//Tell audio manager to play orb collection audio
		AudioManager.PlayOrbCollectionAudio();

		//Tell the game manager that this orb was collected
		GameManager.PlayerGrabbedOrb(this);

		//Deactivate this orb to hide it and prevent further collection
		gameObject.SetActive(false);
	}
}
