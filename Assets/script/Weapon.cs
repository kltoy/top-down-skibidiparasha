using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public float firerate;

    private float lastShootTime = 0;
    private AudioSource audioSource;

    [HideInInspector]
    public Collider2D attackZone;

    protected virtual void Awake()
    {
        attackZone = GetComponent<Collider2D>();
    }

    public abstract void SetRotation(Vector2 dir, float playerScaleX);

    public void SetAudioSource(AudioSource newAudioSource)
    {
        audioSource = newAudioSource;
    }

    public void TryAttack(Vector2 dir)
    {
        if (lastShootTime + firerate < Time.time)
        {
            lastShootTime = Time.time;
            Attack(dir);

            audioSource.Play();
        }
    }

    protected abstract void Attack(Vector2 dir);
}
