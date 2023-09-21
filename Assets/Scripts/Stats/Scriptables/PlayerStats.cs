using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "New Player Stats")]
public class PlayerStats : BasicStats
{
    [Header("Combat Stats")]
    public float damage;
    public float baseAttackDelay;
    public float firstHabilityDelay;
    public float secondHabilityDelay;

    [Header("Resources")]
    public int maxWoodQuantity;
    public int maxFoodQuantity;
    public int maxMagicQuantity;
}
