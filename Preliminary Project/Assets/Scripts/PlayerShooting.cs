using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	[Header("Shooting Properties")]
	public float fireRate = 0.25f;			//Cooldown before the next shot

	float nextFire = 0f;					//Variable to hold shoot cooldown

	PlayerInput input;						//The current inputs for the player
    PlayerMovement movement;                //The current movement for the player
	Animator myAnimator;   

    public GameObject projectile;           //Projectile GameObject

    Vector2 bulletPosition;                 //Holds the bullet position
    public Vector2 bulletOffset = new Vector2(0.7f,0.7f); 	//Offset between the bullet and the player

	Collider2D playerCollider;

	[HideInInspector] public bool shouldFlip;

	void Start ()
	{
		//Get a reference to the required components
		input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
		playerCollider = GetComponent<Collider2D>();
		myAnimator = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		myAnimator.SetBool("shooting",false);
		HandleShoot();
	}

	void HandleShoot()
	{
		shouldFlip = false;

		if ((input.shootPressed || input.shootHeld) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
			myAnimator.SetBool("shooting",true);
			SoundManager.PlaySound("player_shot");

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

			//Detect overlap before instantiating
			Vector2 collisionPosition = new Vector2(bulletPosition.x + bulletOffset.x * playerDirection, bulletPosition.y + bulletOffset.y);
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(collisionPosition, Mathf.Abs(collisionPosition.x - playerCollider.bounds.center.x) - playerCollider.bounds.size.x / 2);

			foreach(Collider2D collider in hitColliders) {
				if(collider != null && collider != playerCollider && !collider.isTrigger) {
					//Debug.Log(collider.ToString());
					return;
				}
			}
            
			GameObject clone = Instantiate(projectile, bulletPosition, Quaternion.identity) as GameObject;
			Bullet bullet = clone.GetComponent<Bullet>();

			bullet.SetProperties(playerDirection, bulletOffset, bulletDirection,this.gameObject);
			//Debug.Log("Shot");
		}
    }
}
