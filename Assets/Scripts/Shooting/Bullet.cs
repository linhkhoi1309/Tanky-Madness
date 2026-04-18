using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float lifetimeSeconds = 3f;

    private BulletAudio bulletAudio;
    private Rigidbody2D rb;
    private Vector2 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletAudio = GetComponent<BulletAudio>();
    }

    void FixedUpdate()
    {
        lastVelocity = rb.linearVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.OnDeath();
            }
            Disable(); // Return bullet to pool instead of destroying it
            return; // Ignore collision with player
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            var normal = collision.GetContact(0).normal;
            var reflected = Vector2.Reflect(lastVelocity, normal).normalized * speed;
            rb.linearVelocity = reflected;
            bulletAudio.PlayBounceSound();
        }
    }

    public void Move(Vector2 shootingDirection)
    {
        if (rb == null)
        {
            Debug.LogError("Bullet missing Rigidbody2D; cannot move.");
            return;
        }
        rb.linearVelocity = shootingDirection.normalized * speed;
        Invoke(nameof(Disable), lifetimeSeconds); // Automatically return to pool after lifetime expires
    }

    public void Disable()
    {
        ObjectPool.Instance.ReturnToPool("Bullet", gameObject);
    }
}
