using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    [HideInInspector]
    public Vector2 directionMove;

    [SerializeField] private float speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out SkibidiController skibidi))
        {
            Destroy(gameObject);
            skibidi.TakeDamage(damage);
        }
    }
    void Update()
    {
        rb.velocity = directionMove * speed;
    }
}
