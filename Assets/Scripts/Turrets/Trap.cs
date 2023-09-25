using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] TurretStats stats;
    private float timer;
    private bool canAttack;

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            canAttack=true;
        }
        timer-= Time.deltaTime;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canAttack) { 
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable?.TakeDamage(stats.damage);
                canAttack = false;
            }
        }
    }
}
