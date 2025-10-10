using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public float destroyAfterSeconds;

    public WeapontScriptableObject weaponData;

    //Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected int currentPrirce;
    protected float currentCooldownDuration;
    private void Awake()
    {
        currentSpeed = weaponData.Speed;
        currentDamage = weaponData.Damage;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPrirce = weaponData.Prirce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
    }
}
