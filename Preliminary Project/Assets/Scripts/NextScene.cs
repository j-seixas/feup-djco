// This script handles detecting collisions with traps and telling the Game Manager
// when the player dies

using UnityEngine;

public class NextScene : MonoBehaviour
{
    int playerLayer;

	void Start()
	{
		playerLayer = LayerMask.NameToLayer("Player");
	} 

	void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.layer == playerLayer)
            GameManager.PlayerReachedNextLevel();
	}
}
