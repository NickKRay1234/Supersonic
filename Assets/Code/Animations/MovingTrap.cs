using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MovingTrap : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    
    public float speed = 2.0f;
    private Transform targetPoint;
    public ParticleSystem collisionParticles;

    private void Start()
    {
        targetPoint = pointA;
        
        transform.DORotate(new Vector3(0, 0, 360), 2.0f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            targetPoint = targetPoint == pointA ? pointB : pointA;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        TriggerCollisionParticles(collider.transform.position);
    }
    
    private void TriggerCollisionParticles(Vector3 collisionPosition)
    {
        collisionParticles.transform.position = collisionPosition;
        collisionParticles.Play();
    }
}


