using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RopeSystem : MonoBehaviour
{

    // 1
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    private bool ropeAttached;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;

    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;
    public LayerMask cancelLayerMask;
    private float ropeMaxCastDistance = 20f;
    private List<Vector2> ropePositions = new List<Vector2>();
    private bool distanceSet;

    public float ropeOffset = 1f;
    public float climbSpeed = 3f;
    private bool isColliding;
    private float lineAngle;

    public bool isSwinging = false;

    void Awake()
    {
        // 2
        ropeJoint.enabled = false;
        playerPosition = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 3
        Vector3 worldMousePosition =
            Camera.main.ScreenToWorldPoint(new Vector3(playerInput.mousePosition.x, playerInput.mousePosition.y, 15f));
        Vector3 facingDirection = worldMousePosition - transform.position;
        float aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        // 4
        Vector3 aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        // 5
        playerPosition = transform.position;

        // 6
        if (!ropeAttached)
        {
            SetCrosshairPosition(aimAngle);
            this.isSwinging = false;
        }
        else 
        {
            this.isSwinging = true;
            playerMovement.ropeHook = ropePositions.Last();
            crosshairSprite.enabled = false;

            //Rotate sprite
            Vector2 lineDirection = playerMovement.ropeHook - (Vector2)transform.position;
            lineAngle = Mathf.Atan2(lineDirection.y, lineDirection.x);
            if(lineAngle < 0) aimAngle = Mathf.PI * 2 + aimAngle;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -1 * (90 - lineAngle * 180 / Mathf.PI));
        }

        HandleInput(aimDirection);
        UpdateRopePositions();
        HandleRopeLength();
    }

    private void SetCrosshairPosition(float aimAngle)
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crosshair.transform.position = crossHairPosition;
    }

        // 1
    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetButtonDown("Hook") && !isSwinging && !playerMovement.isOnGround)
        {
            // 2
            if (ropeAttached) return;
            ropeRenderer.enabled = true;

            RaycastHit2D hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);
            
            // 3
            if (hit.collider != null && !hit.collider.isTrigger)
            {
                isSwinging = true;
                ropeAttached = true;
                if (!ropePositions.Contains(hit.point))
                {
                    // 4
                    ropePositions.Add(hit.point);
                    ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                    ropeJoint.enabled = true;
                    ropeHingeAnchorSprite.enabled = true;
                }
            }
            // 5
            else
            {
                ropeRenderer.enabled = false;
                ropeAttached = false;
                ropeJoint.enabled = false;
            }
        }

        else if (Input.GetButtonDown("Hook") && isSwinging)
        {
            ResetRope();
        }
    }

    // 6
    private void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeAttached = false;
        isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropePositions.Clear();
        ropeHingeAnchorSprite.enabled = false;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
    }

    private void HandleRopeLength()
    {
        // 1
        if (playerInput.vertical >= 1f && ropeAttached && !isColliding)
        {
            ropeJoint.distance -= Time.deltaTime * climbSpeed;
        }
        else if (playerInput.vertical < 0f && ropeAttached)
        {
            ropeJoint.distance += Time.deltaTime * climbSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if hook should be reset
        if(((1<<collision.gameObject.layer) & cancelLayerMask.value) != 0) {
            ResetRope();
        }
    }

    private void OnTriggerStay2D(Collider2D colliderStay)
    {
        if(!colliderStay.isTrigger)
            isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D colliderOnExit)
    {
        if(!colliderOnExit.isTrigger)
            isColliding = false;
    }

    private void UpdateRopePositions()
    {
        // 1
        if (!ropeAttached)
        {
            return;
        }

        // 2
        ropeRenderer.positionCount = ropePositions.Count + 1;

        // 3
        for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                ropeRenderer.SetPosition(i, ropePositions[i]);
                    
                // 4
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                {
                    var ropePosition = ropePositions[ropePositions.Count - 1];
                    if (ropePositions.Count == 1)
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                }
                // 5
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    var ropePosition = ropePositions.Last();
                    ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                // 6
                ropeRenderer.SetPosition(i, transform.position + new Vector3(ropeOffset * Mathf.Cos(lineAngle), ropeOffset * Mathf.Sin(lineAngle), 0));
            }
        }
    }

}
