using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotType
{
    Damage = 0,
    MovingSpeed = 1,
    AtkSpeed = 2
}

public class Potion : MonoBehaviour
{
    [SerializeField] private PotType type;
    [SerializeField] private HUD hud;
    // Start is called before the first frame update

    public delegate void PotionDelegate();
    public static PotionDelegate onDamagePotionEffectRelease;
    public static PotionDelegate onMovingSpeedPotionEffectRelease;
    public static PotionDelegate onAtkSpeedPotionEffectRelease;

    public void ActivateEffect()
    {
        hud.HUDDeactivated();
        switch (type)
        {
            case PotType.Damage:
                onDamagePotionEffectRelease?.Invoke();
                break;
            case PotType.MovingSpeed:
                onMovingSpeedPotionEffectRelease?.Invoke();
                break; 
            case PotType.AtkSpeed:
                onAtkSpeedPotionEffectRelease.Invoke();
                break;
        }
    }
}
