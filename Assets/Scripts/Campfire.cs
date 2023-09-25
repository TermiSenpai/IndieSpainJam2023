using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour, IDamageable
{
    public PlayerStats player;
    public CampfireStats stats;

    public delegate void CampfireDelegate();
    public static CampfireDelegate OnCampfireDieRelease;
    public static CampfireDelegate OnCampfireTakeDamageRelease;

    public Animator anim;

    private void OnEnable()
    {
        DayCycle.NightStartRelease += OnNightStart;
    }

    private void OnDisable()
    {
        DayCycle.NightStartRelease -= OnNightStart;
    }


    public void TakeDamage(float damage)
    {
        stats.currentWood -= 3;
        ChangeAnim();
        OnCampfireTakeDamageRelease?.Invoke();
        CheckHealth();
    }

    void CheckHealth()
    {
        if (stats.currentWood <= 0)
        {
            Die();
        }
    }

    void OnNightStart()
    {
        if (player.currentWood < 3) { Die(); }

        stats.currentWood = player.currentWood;
        ChangeAnim();
    }

    void ChangeAnim()
    {
        anim.SetFloat("Wood", stats.currentWood);
    }

    void Die()
    {
        OnCampfireDieRelease?.Invoke();
    }

}
