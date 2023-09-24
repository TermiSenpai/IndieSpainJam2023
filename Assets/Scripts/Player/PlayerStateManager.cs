using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private MonoBehaviour combatScript;
    [SerializeField] private MonoBehaviour buildScript;
    [SerializeField] private MonoBehaviour recolectionScript;

    private void OnEnable()
    {
        DayCycle.DayStart += OnStartDay;
        DayCycle.EveningStart += OnStartEvening;
        DayCycle.NightStart += OnStartNight;
    }

    private void OnDisable()
    {
        DayCycle.DayStart -= OnStartDay;
        DayCycle.EveningStart -= OnStartEvening;
        DayCycle.NightStart -= OnStartNight;
    }

    private void OnStartDay()
    {
        // Start recolection phase
        recolectionScript.enabled = true;
        combatScript.enabled = false;
        buildScript.enabled = false;
    }

    private void OnStartEvening()
    {
        recolectionScript.enabled = false;
        combatScript.enabled = false;
        // Start build phase
        buildScript.enabled = true;
    }

    private void OnStartNight()
    {
        recolectionScript.enabled = false;
        // Start combat phase
        combatScript.enabled = true;
        buildScript.enabled = false;
    }
}
