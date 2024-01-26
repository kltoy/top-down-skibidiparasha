using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;

    [SerializeField] private Weapon _weapon;

    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Slider health_bar;

    private float health;
    private bool isAlive = true;
    private Joystick joystick;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
        else
        {
            animator.SetBool("isDead", true);
            isAlive = false;
        }
    }
    public void EquipWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        joystick = FindAnyObjectByType<Joystick>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        health = maxHealth;
        health_bar.maxValue = maxHealth;
    }

    private void Update()
    {
        if (isAlive == true)
        {
            rb2D.velocity = joystick.Direction * speed;

            Transform enemy_target = FindEnemy();
            Shoot(enemy_target);

            UpdateUI();
            UpdateAnimatorParameters();
            UpdateFlip();
        }
    }

    private void UpdateUI()
    {
        health_bar.value = health;
    }   

    private void UpdateFlip()
    {
        if (joystick.Horizontal < 0)
            spriteRenderer.flipX = true;

        if (joystick.Horizontal > 0)
            spriteRenderer.flipX = false;
    }

    private void UpdateAnimatorParameters()
    {
        float currentSpeed = joystick.Direction.magnitude;

        if (currentSpeed == 0)
            animator.SetBool("isrun", false);

        if (currentSpeed > 0)
            animator.SetBool("isrun", true);
    }

    private void Shoot(Transform enemy_target)
    {
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

    private Transform FindEnemy()
    {
        Collider2D[] enemyList = Physics2D.OverlapCircleAll(transform.position, _weapon.radius, enemyMask);
        Transform enemy_target = null;
        float mindistance = 1000;


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

        return enemy_target;
    }
}
