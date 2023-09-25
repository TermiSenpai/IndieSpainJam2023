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
    public static CampfireDelegate OnNoCampfireRelease;

    public Animator anim;

    private void OnEnable()
    {
        DayCycle.NightStartRelease += OnNightStart;
        DayCycle.EveningStartRelease += OnNightStart;
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

    void onEveningStart()
    {
        if (player.currentWood < 3)
        {
            OnNoCampfireRelease?.Invoke();
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
