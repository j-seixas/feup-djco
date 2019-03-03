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
    public float bulletOffsetX = 1f;        //Offset between the bullet and the player
    public float bulletOffsetY = 1f;        //Offset between the bullet and the player

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
		if ((input.shootPressed || input.shootHeld) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            
            bulletPosition = transform.position;
            GameObject clone = Instantiate(projectile, bulletPosition, Quaternion.identity) as GameObject;
            Bullet bullet = clone.GetComponent<Bullet>();
            bullet.SetProperties(movement.direction, bulletOffsetX, bulletOffsetY);
            //Debug.Log("Shot");
        }
    }
}
