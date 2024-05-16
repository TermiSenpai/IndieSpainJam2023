using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemyAttack : EnemyAttack
{
    public override void Attack()
    {
        if (canAttack) { }
            

        canAttack = false; 
    }
}
