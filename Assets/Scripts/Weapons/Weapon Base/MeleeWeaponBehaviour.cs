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

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().currentMight;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.TryGetComponent<Breaker>(out Breaker breaker))
            {
                breaker.TakeDamage(GetCurrentDamage()); // make sure to use currentDamage instead of weaponData.Damage
            }
        }
    }
}
