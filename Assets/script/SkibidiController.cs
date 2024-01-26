using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkibidiController : MonoBehaviour
{
    [HideInInspector] public Transform targetTransform;

    [HideInInspector] public float speed;
    [HideInInspector] public float health;
    [HideInInspector] public float damage;
    [HideInInspector] public bool isAlive;
    [HideInInspector] public float rangeAttack;
    [HideInInspector] public float fireRate;

    private Rigidbody2D rb2D;
    private Animator animator;
    private bool canAttack = true;
    private float distanceTargetAndSelf;
    private PlayerController playerController;
    private WaveManager waveManager;

    private void Attack()
    {
        if (distanceTargetAndSelf < rangeAttack && canAttack == true)
        {
            animator.SetTrigger("attack");
            canAttack = false;
            playerController.TakeDamage(damage);
            StartCoroutine(coldown(fireRate));
        }
    }
    private IEnumerator coldown(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        waveManager = FindFirstObjectByType<WaveManager>();
    }

    private void Start()
    {
        isAlive = true;
        playerController =  targetTransform.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isAlive)
        {
            Vector2 betweenTarget = targetTransform.position - transform.position;
            Vector2 direction = betweenTarget.normalized;
            distanceTargetAndSelf = betweenTarget.magnitude;

            rb2D.velocity = direction * speed;

            if (direction.x > 0)
                transform.localScale = new Vector3(-1, 1, 1);

            if (direction.x < 0)
                transform.localScale = new Vector3(1, 1, 1);

            Attack();
        }
    }

    private void Death()
    {
        animator.SetBool("isdead", true);
        Destroy(gameObject, 5);
        isAlive = false;

        waveManager.DeadSkibi();

        Destroy(rb2D);
        Destroy(GetComponent<Collider2D>());
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
            Death();
        }
    }
}
