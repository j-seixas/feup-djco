using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	[Header("Shooting Properties")]
	public float fireRate = 0.25f;			//Cooldown before the next shot

	float nextFire = 0f;					//Variable to hold shoot cooldown

	PlayerInput input;						//The current inputs for the player
    PlayerMovement movement;                //The current movement for the player

    public GameObject projectile;           //Projectile GameObject

    Vector2 bulletPosition;                 //Holds the bullet position
    public Vector2 bulletOffset = new Vector2(1f,1f); 	//Offset between the bullet and the player

	[HideInInspector] public bool shouldFlip;

	void Start ()
	{
		//Get a reference to the required components
		input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
	}

	void FixedUpdate()
	{
		HandleShoot();
	}

	void HandleShoot()
	{
		shouldFlip = false;

		if ((input.shootPressed || input.shootHeld) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;

			//Calculate shooting direction
			Vector3 mousePosition = input.mousePosition;
        	mousePosition.z = 15f;
        	Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        	Vector2 bulletDirection = mousePositionWorld - transform.localPosition; //Initial bullet direction
			
			//The player should flip if it's shooting in a diferent direction in relation to its current direction
			shouldFlip = (movement.direction * bulletDirection.x) < 0;
			int playerDirection = shouldFlip ? -movement.direction : movement.direction;
            bulletPosition = transform.position;

			//Update bullet direction with the bullet offset
			bulletDirection = mousePositionWorld - (transform.localPosition + new Vector3(bulletOffset.x * playerDirection, bulletOffset.y, 0));

			//Impossible shot (deadzone)
			//if((playerDirection * bulletDirection.x) < 0)
			//	return;
            
			GameObject clone = Instantiate(projectile, bulletPosition, Quaternion.identity) as GameObject;
			Bullet bullet = clone.GetComponent<Bullet>();
			bullet.SetProperties(playerDirection, bulletOffset, bulletDirection,this.gameObject);
			//Debug.Log("Shot");
		}
    }
}
