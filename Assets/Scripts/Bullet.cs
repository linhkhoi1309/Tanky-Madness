using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float lifetimeSeconds = 3f;
    private Rigidbody2D rb;
    Vector2 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 shootingDirection)
    {
        if (rb == null)
        {
            Debug.LogError("Bullet missing Rigidbody2D; cannot move.");
            return;
        }
        rb.linearVelocity = shootingDirection.normalized * speed;
        Destroy(gameObject, lifetimeSeconds);
    }

    void FixedUpdate()
    {
        lastVelocity = rb.linearVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy bullet on collision with player
            return; // Ignore collision with player
        }
        var normal = collision.GetContact(0).normal;
        var reflected = Vector2.Reflect(lastVelocity, normal).normalized * speed;
        rb.linearVelocity = reflected;
    }
}
