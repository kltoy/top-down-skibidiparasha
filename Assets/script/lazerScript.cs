using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerScript : Bullet
{
    [SerializeField] private float delayDamage = 1;

    private List<SkibidiController> damableSkibidi = new();

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out SkibidiController enemy) && damableSkibidi.Contains(enemy) == false)
        {
            StartCoroutine(DamageSkibidi(enemy));
        }
    }

    private IEnumerator DamageSkibidi(SkibidiController enemy)
    {
        enemy.TakeDamage(damage);
        damableSkibidi.Add(enemy);
        yield return new WaitForSeconds(delayDamage);
        damableSkibidi.Remove(enemy);
    }
}
