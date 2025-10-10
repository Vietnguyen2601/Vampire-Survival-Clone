using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectable
{
    public int healthResotre;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthResotre);
        Destroy(gameObject);
    }
}
