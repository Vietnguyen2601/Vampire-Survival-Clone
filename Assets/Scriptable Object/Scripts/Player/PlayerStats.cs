using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharactorScriptableObject charactorData;

    //Current stats
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;

    //Spawned Weapon
    public List<GameObject> spawnedWeapons;

    //Experience and Level
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    //Class for definding a level range and the experience cap for that range
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    //I-frame
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;
    void Awake()
    {
        charactorData = CharactorSelections.GetData();
        CharactorSelections.instance.DestroySingleton();

        //Assign the variable
        currentHealth = charactorData.MaxHealth;
        currentRecovery = charactorData.Recovery;
        currentMoveSpeed = charactorData.MoveSpeed;
        currentMight = charactorData.Might;
        currentProjectileSpeed = charactorData.ProjectileSpeed;
        currentMagnet = charactorData.Magnet;

        SpawnWeapon(charactorData.StartingWeapon);
    }

    private void Start()
    {
        // Initialize experience cap based on the first level range
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
    }

    void CheckLevelUp()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }
    public void Kill()
    {
        Debug.Log("Player Died");
    }

    public void RestoreHealth(float amount)
    {
        if (currentHealth < charactorData.MaxHealth)
        {
            currentHealth += amount;

            if (currentHealth > charactorData.MaxHealth)
            {
                currentHealth = charactorData.MaxHealth;
            }
        }
    }

    void Recover()
    {
        if (currentHealth < charactorData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;

            if (currentHealth > charactorData.MaxHealth)
            {
                currentHealth = charactorData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        spawnedWeapons.Add(spawnedWeapon);
    }
}
