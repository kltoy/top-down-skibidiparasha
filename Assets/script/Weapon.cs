using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float ammo;
    public float firerate;
    public float radius;
    public AudioClip shootSound;

    public GameObject bullet_prefab;
    public GameObject VFX;
    public Transform SHOOTPOINT;

    private float lastShootTime = 0;

    private AudioSource audioSource;

    public void SetRotation(Vector2 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 localscale = Vector3.one;

        if (angle > 90 || angle < -90)
            localscale.y = -1;
        else
            localscale.y = 1;

        transform.localScale = localscale;
    }

    public void SetAudioSource(AudioSource newAudioSource)
    {
        audioSource = newAudioSource;
    }

    public void Shoot(Vector2 dir)
    {
        if (lastShootTime + firerate < Time.time)
        {
            lastShootTime = Time.time;
            GameObject bullet = Instantiate(bullet_prefab, SHOOTPOINT.position, transform.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.directionMove = dir.normalized;
            bulletScript.damage = damage;
            Instantiate(VFX, SHOOTPOINT);

            audioSource.PlayOneShot(shootSound, 1);
        }
    }
}
