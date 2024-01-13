using UnityEngine;

public class SkibidiController : MonoBehaviour
{
    [HideInInspector]
    public Transform targetTransform;

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float health;
    [HideInInspector]
    public float damage;
    

    private Rigidbody2D rb2D;
    private Animator animator;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 direction = (targetTransform.position - transform.position).normalized;

        rb2D.velocity = direction * speed;

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void TakeDamage(float damge)
    {
        if (health > 0)
        {
            animator.SetTrigger("hit");
            health = health - damge;
        }
        else 
        {
            animator.SetBool("isdead", true);
            speed = 0;
            Destroy(gameObject, 5);
        }
    }
}
