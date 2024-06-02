using UnityEngine;

public class BloodTrigger : MonoBehaviour
{
    public ParticleSystem collisionParticles;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Blade"))
        {
            TriggerCollisionParticles();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        }
    }

    private void TriggerCollisionParticles()
    {
        if (collisionParticles != null)
            collisionParticles.Play();
    }
}
