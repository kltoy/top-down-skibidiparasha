using UnityEngine;

public class BuleltShotgun : Bullet
{
    [SerializeField] private float speedRost;
    [SerializeField] private float maxRost;

    private bool isTouch = false;

    protected override void Update()
    {
        if (transform.localScale.x < maxRost && isTouch == false)
        {
            Vector3 scale = transform.localScale;
            scale.x = transform.localScale.x + speedRost * Time.deltaTime;
            transform.localScale = scale;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out SkibidiController skibidi) && isTouch == false)
        {
            skibidi.TakeDamage(damage);
            isTouch = true;
        }
    }
}
