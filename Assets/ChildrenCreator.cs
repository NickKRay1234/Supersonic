using UnityEngine;
using System.Collections.Generic;

public class ChildrenCreator : MonoBehaviour
{
    public GameObject childPrefab;
    public int numberOfChildren = 25;
    public float distanceBetweenChildren = 0.05f;

    public List<GameObject> children = new();

    private void Start() => 
        CreateChildren();

    void CreateChildren()
    {
        for (int i = 0; i < numberOfChildren; i++)
        {
            GameObject child = Instantiate(childPrefab, transform);
            child.name = "Child" + (i + 1);

            child.transform.localPosition = new Vector3(i * distanceBetweenChildren, 0, 0);
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                MaterialPropertyBlock block = new MaterialPropertyBlock();
                block.SetColor("_Color", new Color(Random.value, Random.value, Random.value));
                renderer.SetPropertyBlock(block);
            }
            children.Add(child);
        }
    }
}



