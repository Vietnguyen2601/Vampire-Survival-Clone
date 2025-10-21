using UnityEngine;
using UnityEngine.Playables;

public class ProjectileWeaponBehavior : MonoBehaviour
{
    public WeapontScriptableObject weaponData;

    protected Vector3 direction;
    public float detroyAfterSeconds;

    //Current stats
    protected float currentSpeed;
    protected float currentDamage;
    protected float currentCooldownDuration;
    protected int currentPrirce;

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
        Destroy(gameObject, detroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) //left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry < 0) //down
        {
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry > 0) //up
        {
            scale.x = scale.x * -1;
        }
        else if (dirx > 0 && diry > 0) //right up
        {
            rotation.z = 0f;
        }
        else if (dirx > 0 && diry < 0) //right down
        {
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry > 0) //left up
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry < 0) //left down
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }


        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage()); // make sure to use currentDamage instead of weaponData.Damage
            ReducePrirce();
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.TryGetComponent<Breaker>(out Breaker breaker))
            {
                breaker.TakeDamage(GetCurrentDamage()); // make sure to use currentDamage instead of weaponData.Damage
                ReducePrirce();
            }
        }
    }

    void ReducePrirce()
    {
        currentPrirce--;
        if (currentPrirce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
