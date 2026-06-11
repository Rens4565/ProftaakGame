using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int BulletDamage = 10;

    private Transform target;

    private bool hasHit = false; 

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (hasHit) return; 

        Health health = other.gameObject.GetComponent<Health>();

        if (health != null)
        {
            hasHit = true;

            health.TakeDamage(BulletDamage);

            Destroy(gameObject);
        }
        
    }
}