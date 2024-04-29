using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Stats/New Campfire Stats")]
public class CampfireStats : BasicStats
{
    public int currentWood;
    public AudioClip onFireClip;
}
