using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : Weapon
{
    public GameObject VFX;
    public Collider2D attackZone;
    public Animator animator;
    bool isAttack = false;

    private List<SkibidiController> damableSkibidi = new();

    protected override void Attack(Vector2 dir)
    {
        isAttack = true;
    }

    void Update()
    {
        animator.SetBool("IsAttack", isAttack);
        if (isAttack == true)
        {
            List<Collider2D> list = new List<Collider2D>();
            Physics2D.OverlapCollider(attackZone, list);
            foreach (Collider2D collider in list)
            {
                if (collider.TryGetComponent(out SkibidiController enemy) && damableSkibidi.Contains(enemy) == false)
                {
                    StartCoroutine(DamageSkibidi(enemy));
                }
            }

            if (audioSourceShoot.isPlaying == false)
            {
                PlaySound();
            }
        }
    }

    public override void EmptyAttackZone()
    {
        isAttack = false;
        StopSoundShoot();
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

    private IEnumerator DamageSkibidi(SkibidiController enemy)
    {
        enemy.TakeDamage(damage);
        damableSkibidi.Add(enemy);
        yield return new WaitForSeconds(1);
        damableSkibidi.Remove(enemy);
    }
}