using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
	[Header("Shooting Properties")]
	public float fireRate = 0.25f;			//Cooldown before the next shot
    public float maxDistance = 20f;         //Maximum shooting distance
    public int bulletsPerRound = 12;        //Bullets per round
    public float reloadTime = 3f;           //Reload time
	public float accuracy = 1f;				//Enemy shooting accuracy

    private bool reloading;                 //Is the enemy reloading?
    private int bulletCounter;              //Bullet counter for rounds
    private float reloadCounter;            //Reload counter

	float nextFire = 0f;					//Variable to hold shoot cooldown
    public GameObject projectile;           //Projectile GameObject

    Vector2 bulletPosition;                 //Holds the bullet position
    public Vector2 bulletOffset = new Vector2(0.7f,0.7f); 	//Offset between the bullet and the player

	Collider2D enemyCollider;
    public GameObject player;
    private SpriteRenderer sprite;

	private BoxCollider2D playerCollider;


    private int direction;

	void Start ()
	{
		//Get a reference to the required components
		enemyCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
		playerCollider = player.GetComponent<BoxCollider2D>();
        bulletCounter = 0;
        reloadCounter = 0f;
        direction = 1;
	}

	void FixedUpdate()
	{
		HandleShoot();
	}

	void HandleShoot()
	{
        //Can't shoot, reload
        if(!reloading && bulletCounter >= bulletsPerRound) {
            reloadCounter = Time.time + reloadTime;
            reloading = true;
        }

        //Reload finished
        if(reloading && Time.time >= reloadCounter) {
            bulletCounter = 0;
            reloading = false;
        }

		Vector2 playerCenter = playerCollider.bounds.center;
        float distance = Vector3.Distance(playerCenter, transform.position);

		if (distance < maxDistance && Time.time > nextFire && !reloading) {
            nextFire = Time.time + fireRate;

			//Calculate shooting direction
            Vector3 shootingTarget = playerCenter;
			//Add random noise
			shootingTarget += (Vector3)Random.insideUnitCircle * accuracy;
			Vector2 bulletDirection = shootingTarget - transform.position; //Initial bullet direction
			
			//The player should flip if it's shooting in a diferent direction in relation to its current direction
			bool shouldFlip = (direction * bulletDirection.x) < 0;
			int playerDirection = shouldFlip ? -direction : direction;
            bulletPosition = transform.position;

			//Update bullet direction with the bullet offset
			bulletDirection = shootingTarget - (transform.position + new Vector3(bulletOffset.x * playerDirection, bulletOffset.y, 0));

			//Impossible shot (deadzone)
			//if((playerDirection * bulletDirection.x) < 0)
			//	return;

			//Detect overlap before instantiating
			Vector2 collisionPosition = new Vector2(bulletPosition.x + bulletOffset.x * playerDirection, bulletPosition.y + bulletOffset.y);
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(collisionPosition, Mathf.Abs(collisionPosition.x - enemyCollider.bounds.center.x) - enemyCollider.bounds.size.x / 2);

			foreach(Collider2D collider in hitColliders) {
				if(collider != null && collider != enemyCollider && !collider.isTrigger) {
					//Debug.Log(collider.ToString());
					return;
				}
			}

            bulletCounter++;
            
			GameObject clone = Instantiate(projectile, bulletPosition, Quaternion.identity) as GameObject;
            clone.layer = LayerMask.NameToLayer("EnemyBullets");
			Bullet bullet = clone.GetComponent<Bullet>();
			bullet.SetProperties(playerDirection, bulletOffset, bulletDirection,this.gameObject);
			//Debug.Log("Shot");

            if(shouldFlip) {
                sprite.flipX = !sprite.flipX;
                direction *= -1;
            }
		}
    }
}
