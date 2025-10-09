using UnityEngine;

public class ShieldController : WeaponController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();    
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnShield = Instantiate(prefab);
        spawnShield.transform.position = transform.position;
        spawnShield.transform.parent = transform;
    }
}
