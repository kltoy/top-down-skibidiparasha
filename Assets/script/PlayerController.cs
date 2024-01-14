using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;

    [SerializeField] private Weapon _weapon;

    [SerializeField] private LayerMask enemyMask;

    private float health;

    private Joystick joystick;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        joystick = FindAnyObjectByType<Joystick>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        rb2D.velocity = joystick.Direction * speed;

        if (joystick.Horizontal < 0)
            spriteRenderer.flipX = false;

        if (joystick.Horizontal > 0)
            spriteRenderer.flipX = true;

        float currentSpeed = joystick.Direction.magnitude;

        if (currentSpeed == 0)
            animator.SetBool("isrun", false);

        if (currentSpeed > 0)
            animator.SetBool("isrun", true);

        Collider2D[] enemyList = Physics2D.OverlapCircleAll(transform.position, _weapon.radius, enemyMask);

        float mindistance = 1000;
        Transform enemy_target = null;

        foreach (Collider2D enemy in enemyList)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            SkibidiController skibidiController = enemy.gameObject.GetComponent<SkibidiController>();

            if (distance < mindistance && skibidiController.isAlive == true)
            {
                mindistance = distance;
                enemy_target = enemy.transform;
            }
        }

        if (enemy_target != null)
        {
            if (enemy_target.position.x > transform.position.x)
                spriteRenderer.flipX = false;

            if (enemy_target.position.x < transform.position.x)
                spriteRenderer.flipX = true;

            var dir = enemy_target.position - transform.position;

            _weapon.SetRotation(dir);
            _weapon.Shoot(dir);
        }
    }


    public void EquipWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }
}
