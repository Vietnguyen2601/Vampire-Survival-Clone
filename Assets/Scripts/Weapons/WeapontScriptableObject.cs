using UnityEngine;

[CreateAssetMenu(fileName = "WeapontScriptableObject", menuName = "ScriptatbleObject/Weapon")] 
public class WeapontScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    //base stats of the weapon
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    int prirce;
    public int Prirce { get => prirce; private set => prirce = value; }
}
