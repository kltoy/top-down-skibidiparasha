using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public float firerate;

    private float lastShootTime = 0;
    protected AudioSource audioSourceShoot;

    public Collider2D visionZone;

    public abstract void SetRotation(Vector2 dir, float playerScaleX);

    protected virtual void Awake()
    {
        audioSourceShoot = GetComponentInChildren<AudioSource>();
    }

    public void TryAttack(Vector2 dir)
    {
        if (lastShootTime + firerate < Time.time)
        {
            lastShootTime = Time.time;
            Attack(dir);
        }
    }

    protected virtual void PlaySound()
    {
        audioSourceShoot.Play();
    }

    protected void StopSoundShoot()
    {
        audioSourceShoot.Stop();
    }

    public virtual void EmptyAttackZone()
    {

    }

    protected abstract void Attack(Vector2 dir);
}
