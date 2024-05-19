using UnityEngine;

public class Gun : Weapon
{
    public GameObject bullet_prefab;
    public GameObject VFX;
    public Transform SHOOTPOINT;
    public float maxSpreadAngle;

    public override void SetRotation(Vector2 dir, float playerScaleX)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 localscale = Vector3.one;

        if (angle > 90 || angle < -90)
            localscale.y = -1;
        else
            localscale.y = 1;

        localscale.x = playerScaleX;

        transform.localScale = localscale;
    }


    protected override void Attack(Vector2 dir)
    {
        // Maximum spread angle in degrees

        // Calculate base direction (normalized)
        Vector2 baseDir = dir.normalized;

        // Apply random spread to the base direction
        float randomSpread = Random.Range(-maxSpreadAngle, maxSpreadAngle); // Random spread angle
        Vector2 spreadDir = Quaternion.AngleAxis(randomSpread, Vector3.forward) * baseDir;

        // Instantiate bullet
        GameObject bullet = Instantiate(bullet_prefab, SHOOTPOINT.position, transform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // Set bullet properties
        bulletScript.directionMove = spreadDir.normalized; // Apply spread direction
        bulletScript.damage = damage;

        // Instantiate visual effect
        Instantiate(VFX, SHOOTPOINT);
        PlaySound();
    }

}




