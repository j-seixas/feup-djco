// This script handles detecting collisions with traps and telling the Game Manager
// when the player dies

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	static public int initialHealth = 5;
	public int health = initialHealth;
	public bool isAlive = true;
	private int trapLayer;
	private int enemyBulletsLayer;
	public BoxCollider2D worldCollider;

	void Start()
	{
		trapLayer = LayerMask.NameToLayer("Traps");
		enemyBulletsLayer = LayerMask.NameToLayer("EnemyBullets");
		GameManager.SetPlayerHealth(this);
		health = GameManager.GiveHP();
		HUD.SetPlayerHealth(this);
	} 

	void Update() {
		//No health left, the player is dead
		if(health <= 0) {
			isAlive = false;
			SoundManager.PlaySound("hit");
			gameObject.SetActive(false); //Disable player game object
			//Debug.Log("Player died");
			GameManager.PlayerDied();
		}
	}

	public int GetHP()
	{
		return health;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(!isAlive)
			return;

		if (trapLayer == collision.gameObject.layer) {
			//Falling platform is still falling
			if(!collision.gameObject.GetComponent<FallingPlatform>().hasFinishedFalling)
				health = -1;
	
		}
		else if (enemyBulletsLayer == collision.gameObject.layer){
			health--;
			SoundManager.PlaySound("hit");
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(!isAlive)
			return;

		//Player exceeded world bounds
		if(worldCollider == collider)
			health = -1;
	}
}
