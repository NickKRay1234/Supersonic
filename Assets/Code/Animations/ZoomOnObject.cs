using UnityEngine;
using DG.Tweening;

public class ZoomOnObject : MonoBehaviour
{
    public Transform targetObject; 
    public float zoomScale = 2f; 
    public float zoomDuration = 1f; 
    public float cameraMoveDuration = 1f; 

    private Camera mainCamera;
    private Vector3 initialCameraPosition;
    private Vector3 initialObjectScale;

    void Start()
    {
        mainCamera = Camera.main;
        initialCameraPosition = mainCamera.transform.position;
        initialObjectScale = targetObject.localScale;
    }

    public void ZoomIn()
    {
        targetObject.DOScale(initialObjectScale * zoomScale, zoomDuration);
        
        mainCamera.transform.DOMove(targetObject.position + new Vector3(0, 0, -10), cameraMoveDuration);
    }

    public void ZoomOut()
    {
        targetObject.DOScale(initialObjectScale, zoomDuration);
        
        mainCamera.transform.DOMove(initialCameraPosition, cameraMoveDuration);
    }
}

