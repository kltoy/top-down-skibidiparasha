using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drobash : Gun
{
    [SerializeField] private int bulletsCountInShoot;

    protected override void Attack(Vector2 dir)
    {
        // Calculate base direction (normalized)
        Vector2 baseDir = dir.normalized;

        for (int i = 0; i < bulletsCountInShoot; i++)
        {
            // Apply random spread to the base direction
            float randomSpread = Random.Range(-maxSpreadAngle, maxSpreadAngle); // Random spread angle
            Vector2 spreadDir = Quaternion.AngleAxis(randomSpread, Vector3.forward) * baseDir;

            // Instantiate bullet
            GameObject bullet = Instantiate(bullet_prefab, SHOOTPOINT.position, transform.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            // Set bullet properties
            bulletScript.directionMove = spreadDir.normalized; // Apply spread direction
            bulletScript.damage = damage;
        }
        // Instantiate visual effect
        Instantiate(VFX, SHOOTPOINT);
    }
}
