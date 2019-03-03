using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float velX = 7.5f;
    public float velY = 0f;

    public float destroyTimeout = 10f; 
    
    Rigidbody2D rigidBody;

	void Start ()
	{
        Destroy(this.gameObject, destroyTimeout); //Destroy after a timeout
	}

    public void SetProperties(int flip, float offsetX, float offsetY)
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0f;
        rigidBody.velocity = new Vector2(velX * flip, velY);
        transform.localPosition += new Vector3(offsetX * flip, offsetY, 0);
        transform.localScale = new Vector3(flip * transform.localScale.x, transform.localScale.y, 1);
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        Destroy(this.gameObject); //Destroy on collision
        //Debug.Log("Collision");
    }
    
}
