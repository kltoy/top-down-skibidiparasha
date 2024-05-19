using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    [HideInInspector]
    public Vector2 directionMove;

    [SerializeField] private float speed;

    private Rigidbody2D rb;

    [SerializeField] private float BulletDelay;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, BulletDelay);

        // Нормализуем вектор направления, чтобы получить единичный вектор
        Vector2 lookDirection = directionMove.normalized;

        // Вычисляем угол в радианах между направлением вправо (1, 0) и lookDirection
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // Поворачиваем спрайт к указанному углу
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out SkibidiController skibidi))
        {
            Destroy(gameObject);
            skibidi.TakeDamage(damage);
        }
    }
    protected virtual void Update()
    {
        rb.velocity = directionMove * speed;
    }
}
