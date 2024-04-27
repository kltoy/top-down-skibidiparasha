using UnityEngine;

public class Gun : Weapon
{
    public GameObject bullet_prefab;
    public GameObject VFX;
    public Transform SHOOTPOINT;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

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
        GameObject bullet = Instantiate(bullet_prefab, SHOOTPOINT.position, transform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.directionMove = dir.normalized;
        bulletScript.damage = damage;
        Instantiate(VFX, SHOOTPOINT);
    }

}

