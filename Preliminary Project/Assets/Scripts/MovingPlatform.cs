using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;
    public DistanceJoint2D distanceJoint;
    public BoxCollider2D boxCollider;
    private Vector3 anchor;

    void Start()
    {
        lineRenderer1.positionCount = 2;
        lineRenderer2.positionCount = 2;
        anchor = distanceJoint.connectedAnchor;
        lineRenderer1.SetPosition(1, anchor - new Vector3(boxCollider.size.x, 0, 0));
        lineRenderer2.SetPosition(1, anchor + new Vector3(boxCollider.size.x, 0, 0));
        UpdatePositions();
    }

    void Update()
    {
        UpdatePositions();
    }

    void UpdatePositions()
    {
        Vector3 leftSide = new Vector3(boxCollider.bounds.center.x - boxCollider.size.x, boxCollider.bounds.center.y, 0);
        Vector3 rightSide = new Vector3(boxCollider.bounds.center.x + boxCollider.size.x, boxCollider.bounds.center.y, 0);
        lineRenderer1.SetPosition(0, leftSide);
        lineRenderer2.SetPosition(0, rightSide);
    }
}
