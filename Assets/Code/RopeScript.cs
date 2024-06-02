using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public Transform player;
    public LineRenderer rope;
    public LayerMask collMask;
    public float detachDistance = 0.5f; // Минимальное расстояние для отцепления точек

    public List<Vector3> ropePositions { get; set; } = new List<Vector3>();

    private void Awake() => AddPosToRope(gameObject.transform.position);

    private void Update()
    {
        UpdateRopePositions();
        LastSegmentGoToPlayerPos();

        DetectCollisionEnter();
        if (ropePositions.Count > 2) DetectCollisionExits();

        CheckAndDetachPoints();
    }

    private void DetectCollisionEnter()
    {
        RaycastHit hit;
        if (Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 2), out hit, collMask))
        {
            ropePositions.RemoveAt(ropePositions.Count - 1);
            AddPosToRope(hit.point);
        }
    }

    private void DetectCollisionExits()
    {
        RaycastHit hit;
        if (!Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 3), out hit, collMask))
        {
            ropePositions.RemoveAt(ropePositions.Count - 2);
        }
    }

    private void AddPosToRope(Vector3 _pos)
    {
        ropePositions.Add(_pos);
        ropePositions.Add(player.position); // Всегда последняя точка должна быть позицией игрока
    }

    private void UpdateRopePositions()
    {
        rope.positionCount = ropePositions.Count;
        rope.SetPositions(ropePositions.ToArray());
    }

    private void LastSegmentGoToPlayerPos() => rope.SetPosition(rope.positionCount - 1, player.position);

    private void CheckAndDetachPoints()
    {
        for (int i = ropePositions.Count - 2; i > 0; i--)
        {
            if (Vector3.Distance(new Vector3(ropePositions[i].x, ropePositions[i].y), 
                                 new Vector3(ropePositions[i - 1].x, ropePositions[i - 1].y)) < detachDistance)
            {
                ropePositions.RemoveAt(i);
            }
        }
    }
}
