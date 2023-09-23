using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Stats/New Enemy Stats")]
public class EnemyStats : BasicStats
{
    [Header("Combat stats")]
    public float radiusRange;
    public float hitDelay;
    public float attackLength;
    public LayerMask damageableLayer;
}
