using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Stats/New Player Stats")]
public class PlayerStats : BasicStats
{
    [Header("Combat Stats")]
    public float raidusRange;
    public float baseAttackDelay;
    public float firstHabilityDelay;
    public float secondHabilityDelay;
    public LayerMask enemyLayer;

    [Header("Resources")]
    public int maxWoodQuantity;
    public int maxFoodQuantity;
    public float maxMagicQuantity;

    [Header("Inventory")]
    public int currentWood;
    public int currentFood;
    public float currentMagic;

    public static PlayerStats Instance { get; set; }

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }
}
