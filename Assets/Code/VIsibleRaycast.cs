using System.Collections.Generic;
using UnityEngine;

public class VisibleRaycast2D : MonoBehaviour
{
    public Transform player;
    public LineRenderer rope;
    public LayerMask collMask;
    public float minDistanceToAddPoint = 0.1f;
    public float maxDistanceToRemovePoint = 0.1f;
    public float removeAngleThreshold = -60f;

    private List<Vector3> ropePositions { get; } = new List<Vector3>();
    private Vector3 lastPosition;

    private void Awake() {
        lastPosition = transform.position; 
        AddPosToRope(transform.position);
    }

    private void Update()
    {
        UpdateRopePositions();
        DetectCollisionEnter();
        CheckToRemoveLastPoint();
    }

    private void DetectCollisionEnter()
    {
        Vector3 lastSegment = rope.GetPosition(ropePositions.Count - 1);
        RaycastHit2D hit = Physics2D.Linecast(player.position, lastSegment, collMask);
        if (hit.collider == null) return;
        if (Vector3.Distance(lastPosition, hit.point) < minDistanceToAddPoint) return;
        AddPosToRope(hit.point);
        lastPosition = hit.point;
    }

    private void AddPosToRope(Vector3 positionPoint) => ropePositions.Add(positionPoint);

    private void UpdateRopePositions()
    {
        rope.positionCount = ropePositions.Count + 1;
        rope.SetPositions(ropePositions.ToArray());
        rope.SetPosition(ropePositions.Count, player.position);
    }

    private void CheckToRemoveLastPoint()
    {
        if (ropePositions.Count < 2) return;

        // Check the distance between the player and the last rope point
        Vector3 lastRopePosition = ropePositions[ropePositions.Count - 1];
        Vector3 secondLastRopePosition = ropePositions[ropePositions.Count - 2];

        // Calculate the direction vectors
        Vector3 directionToLastPoint = (lastRopePosition - player.position).normalized;
        Vector3 directionToSecondLastPoint = (secondLastRopePosition - lastRopePosition).normalized;

        // Calculate the angle between the direction vectors
        float angle = Vector3.Angle(directionToLastPoint, directionToSecondLastPoint);

        // Check if the player is moving towards the second last point
        if (angle > removeAngleThreshold || Vector3.Distance(player.position, lastRopePosition) < maxDistanceToRemovePoint)
        {
            // Remove the last rope point
            ropePositions.RemoveAt(ropePositions.Count - 1);
            lastPosition = ropePositions[ropePositions.Count - 1];
        }
    }
    
    public List<Vector3> GetRopePositions()
    {
        return new List<Vector3>(ropePositions);
    }
}