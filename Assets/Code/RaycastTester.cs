using UnityEngine;

public class RaycastTester : MonoBehaviour
{
    public Transform startNode;
    public Transform endNode;
    public LayerMask hitLayers;
    public float maxDistance = 20f;

    void Update()
    {
        Vector3 direction = endNode.position - startNode.position;
        Debug.DrawRay(startNode.position, direction.normalized * maxDistance, Color.blue);

        RaycastHit hit;
        if (Physics.Raycast(startNode.position, direction, out hit, maxDistance, hitLayers))
        {
            Debug.Log($"Test Raycast hit at: {hit.point}");
        }
        else
        {
            Debug.Log("Test Raycast did not hit anything.");
        }
    }
}