using Mono.Cecil;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;

    [SerializeField] private Weapon _weapon;

    [SerializeField] private LayerMask enemyMask;

    private float health;
    private float lastShootTime;

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

            if (distance < mindistance)
            {
                mindistance = distance;
                enemy_target = enemy.transform;
            }
        }

        if (enemy_target != null)
        {
            if (enemy_target.position.x > transform.position.x)
                spriteRenderer.flipX = true;

            if (enemy_target.position.x < transform.position.x)
                spriteRenderer.flipX = false;

            var dir = enemy_target.position - _weapon.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            _weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Shoot(dir );
        }
    }
    private void Shoot(Vector2 dir)
    {
     
        if (lastShootTime + _weapon.firerate < Time.time)
        {
            lastShootTime = Time.time;
            GameObject bullet = Instantiate(_weapon.bullet_prefab, _weapon.SHOOTPOINT.position, _weapon.transform.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.directionMove = dir.normalized;
            bulletScript.damage = _weapon.damage;
            Instantiate(_weapon.VFX, _weapon.SHOOTPOINT);
        }
    }
    
    public void EquipWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }
}
