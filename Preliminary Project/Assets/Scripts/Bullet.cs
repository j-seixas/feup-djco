using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float velocity = 7.5f;
    public float destroyTimeout = 10f; 
    
    Rigidbody2D rigidBody;

	void Start ()
	{
        Destroy(this.gameObject, destroyTimeout); //Destroy after a timeout
	}

    public void SetProperties(int flip, Vector2 offset, Vector2 direction, GameObject source)
    {
        //Ignore collision with the source
        BoxCollider2D sourceCollider = source.GetComponent<BoxCollider2D>();
        BoxCollider2D thisCollider = this.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(sourceCollider, thisCollider);

        //Setup
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0f;
        rigidBody.velocity = new Vector2(velocity * direction.normalized.x, velocity * direction.normalized.y);
        transform.localPosition += new Vector3(offset.x * flip, offset.y, 0);
        //transform.localScale = new Vector3(flip * transform.localScale.x, transform.localScale.y, 1);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        Destroy(this.gameObject); //Destroy on collision
        //Debug.Log("Collision");
    }
    
}
