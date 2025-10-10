using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject prefab; 
    public WeapontScriptableObject weapon;  
    float currentCooldown;


    protected PlayerMovement pm;

    protected virtual void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>(); 
        currentCooldown = weapon.CooldownDuration;
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weapon.CooldownDuration;
    }
}
