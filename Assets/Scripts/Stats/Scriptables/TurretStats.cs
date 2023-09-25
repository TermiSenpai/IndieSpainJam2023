using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Stats/New Turret Stats")]
public class TurretStats : BasicStats
{
    [Header("INFO")]
    public string TurretName;
    public string description;
    public GameObject turretGameobject;
    public GameObject Proyectil;

    [Header("Combat Stats")]
    public float attackDelay;
    public float rangeRadius;
    public LayerMask enemyLayer;
}
