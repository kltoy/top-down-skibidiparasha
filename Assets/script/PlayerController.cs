using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;

    //источники звуков
    [SerializeField] private AudioSource audioSourceShoot;
    [SerializeField] private AudioSource audioSourceCoinCollect;

    private Weapon _weapon;

    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Slider health_bar;

    private float health;
    private bool isAlive = true;
    private Joystick joystick;
    private Animator animator;
    private Rigidbody2D rb2D;

    public GameObject panel;
    public WaveManager waveManager;
    public float balance;
    public TMP_Text recordText;
    public GameObject newRecordNotify;
    public TMP_Text coinBalance;

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
        else if (isAlive == true)
        {
            Death();
        }
    }

    private void Death()
    {
        animator.SetBool("isDead", true);
        isAlive = false;
        panel.SetActive(true);

        GameMode ActiveMode = waveManager.GetActiveMode();

        print(ActiveMode.mode);

        switch (ActiveMode.mode)
        {
            case GameMode.GameModes.Easy:
                if (waveManager.wavenumber > YandexGame.savesData.easyrecords)
                {
                    YandexGame.savesData.easyrecords = waveManager.wavenumber;
                    YandexGame.SaveProgress();
                    newRecordNotify.SetActive(true);
                }

                recordText.text = YandexGame.savesData.easyrecords.ToString();
                break;

            case GameMode.GameModes.Normal:
                if (waveManager.wavenumber > YandexGame.savesData.normalrecords)
                {
                    YandexGame.savesData.normalrecords = waveManager.wavenumber;
                    YandexGame.SaveProgress();
                    newRecordNotify.SetActive(true);
                }

                recordText.text = YandexGame.savesData.normalrecords.ToString();
                break;

            case GameMode.GameModes.Hard:
                if (waveManager.wavenumber > YandexGame.savesData.hardrecords)
                {
                    YandexGame.savesData.hardrecords = waveManager.wavenumber;
                    YandexGame.SaveProgress();
                    newRecordNotify.SetActive(true);
                }

                recordText.text = YandexGame.savesData.hardrecords.ToString();
                break;
        }

        
    }

    public void EquipWeapon(Weapon weapon)
    {
        UnequipWeapon();

        _weapon = weapon;
        _weapon.SetAudioSource(audioSourceShoot);
    }

    private void UnequipWeapon()
    {
        if (_weapon != null) {
            Destroy(_weapon.gameObject);
        }
        

    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        joystick = FindAnyObjectByType<Joystick>();
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
            UpdateScaleX();

            Transform enemy_target = FindEnemy();
            Shoot(enemy_target);

            UpdateUI();
            UpdateAnimatorParameters();
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlive == true && collision.gameObject.TryGetComponent(out Coin coin)) 
        {
            balance = balance + coin.balance;
            coinBalance.text = balance.ToString();
            audioSourceCoinCollect.Play();
            Destroy(coin.gameObject);
        }
    }

    private void UpdateUI()
    {
        health_bar.value = health;
    }   

    private void UpdateScaleX()
    {
        Vector3 localscale = transform.localScale;

        if (joystick.Horizontal < 0)
            localscale.x = -1;

        if (joystick.Horizontal > 0)
            localscale.x = 1;

        transform.localScale = localscale;
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
            Vector3 localscale = transform.localScale;

            if (enemy_target.position.x > transform.position.x)
                localscale.x = 1;

            if (enemy_target.position.x < transform.position.x)
                localscale.x = -1;


            transform.localScale = localscale;
            var dir = enemy_target.position - transform.position;

            _weapon.SetRotation(dir, localscale.x);
            _weapon.TryAttack(dir);
        }
    }

    private Transform FindEnemy()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.layerMask = enemyMask;
        Collider2D[] enemyList = new Collider2D[50];

        Physics2D.OverlapCollider(_weapon.attackZone, contactFilter, enemyList);

        Transform enemy_target = null;
        float mindistance = 1000;

        foreach (Collider2D enemy in enemyList)
        {
            if (enemy == null) continue;

            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            SkibidiController skibidiController = enemy.gameObject.GetComponent<SkibidiController>();

            if (skibidiController && distance < mindistance && skibidiController.isAlive == true)
            {
                mindistance = distance;
                enemy_target = enemy.transform;
            }
        }

        return enemy_target;
    }
}
