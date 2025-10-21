using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;

    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());

            markedEnemies.Add(col.gameObject);
        }
        else if (col.CompareTag("Prop") && !markedEnemies.Contains(col.gameObject))
        {
            if (col.TryGetComponent<Breaker>(out Breaker breaker))
            {
                breaker.TakeDamage(GetCurrentDamage()); // make sure to use currentDamage instead of weaponData.Damage
                markedEnemies.Add(col.gameObject);
            }
        }
    }
}
