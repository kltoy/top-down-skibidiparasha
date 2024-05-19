using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
   private Animator _animator;


    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
        base.Awake();
    }
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
        Vector3 localscale = Vector3.one;

        if (angle > 90 || angle < -90)
            localscale.y = -1;
        else
            localscale.y = 1;

        transform.localScale = localscale;
    }

    protected override void Attack(Vector2 dir)
    {
        _animator.SetTrigger("attack");
        List<Collider2D> collders = new();

        Physics2D.OverlapCollider(visionZone, collders);

        foreach (Collider2D coll in collders) {
            if (coll.gameObject.TryGetComponent(out SkibidiController skibidi))
            {
                skibidi.TakeDamage(damage);
            }
        }

        PlaySound();
    }
}
