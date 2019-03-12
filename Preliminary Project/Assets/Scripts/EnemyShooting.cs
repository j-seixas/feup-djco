using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
	[Header("Shooting Properties")]
	public float fireRate = 0.25f;			//Cooldown before the next shot
    public float maxDistance = 20f;         //Maximum shooting distance
    public int bulletsPerRound = 12;        //Bullets per round
    public float reloadTime = 3f;           //Reload time

    private bool reloading;                 //Is the enemy reloading?
    private int bulletCounter;              //Bullet counter for rounds
    private float reloadCounter;            //Reload counter

	float nextFire = 0f;					//Variable to hold shoot cooldown
    public GameObject projectile;           //Projectile GameObject

    Vector2 bulletPosition;                 //Holds the bullet position
    public Vector2 bulletOffset = new Vector2(0.7f,0.7f); 	//Offset between the bullet and the player

	Collider2D enemyCollider;
    public GameObject player;

	[HideInInspector] public bool shouldFlip;

	void Start ()
	{
		//Get a reference to the required components
		enemyCollider = GetComponent<Collider2D>();
        bulletCounter = 0;
        reloadCounter = 0f;
	}

	void FixedUpdate()
	{
		HandleShoot();
	}

	void HandleShoot()
	{
		shouldFlip = false;

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

        float distance = Vector3.Distance(player.transform.position, transform.position);

		if (distance < maxDistance && Time.time > nextFire && !reloading) {
            nextFire = Time.time + fireRate;

			//Calculate shooting direction
            Vector3 shootingTarget = player.transform.position;
			Vector2 bulletDirection = shootingTarget - transform.localPosition; //Initial bullet direction
			
			//The player should flip if it's shooting in a diferent direction in relation to its current direction
			//shouldFlip = (movement.direction * bulletDirection.x) < 0;
			//int playerDirection = shouldFlip ? -movement.direction : movement.direction;
            bulletPosition = transform.position;

			//Update bullet direction with the bullet offset
			//bulletDirection = mousePositionWorld - (transform.localPosition + new Vector3(bulletOffset.x * playerDirection, bulletOffset.y, 0));

			//Impossible shot (deadzone)
			//if((playerDirection * bulletDirection.x) < 0)
			//	return;

			//Detect overlap before instantiating
			Vector2 collisionPosition = new Vector2(bulletPosition.x + bulletOffset.x /** playerDirection*/, bulletPosition.y + bulletOffset.y);
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
			bullet.SetProperties(1/*playerDirection*/, bulletOffset, bulletDirection,this.gameObject);
			//Debug.Log("Shot");
		}
    }
}
