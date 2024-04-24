using UnityEngine;

public enum PlayerState
{
    Recolection,
    Build,
    Combat
}

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;

    [SerializeField] private MonoBehaviour recolectionScript;
    [SerializeField] private MonoBehaviour buildScript;
    [SerializeField] private MonoBehaviour combatScript;

    private PlayerState currentState = PlayerState.Recolection;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        DayCycle.DayStartRelease += OnStartDay;
        DayCycle.EveningStartRelease += OnStartEvening;
        DayCycle.NightStartRelease += OnStartNight;
    }

    private void OnDisable()
    {
        DayCycle.DayStartRelease -= OnStartDay;
        DayCycle.EveningStartRelease -= OnStartEvening;
        DayCycle.NightStartRelease -= OnStartNight;
    }

    private void OnStartDay()
    {
        SwitchState(PlayerState.Recolection);
    }

    private void OnStartEvening()
    {
        SwitchState(PlayerState.Build);
    }

    private void OnStartNight()
    {
        SwitchState(PlayerState.Combat);
    }

    public void SwitchState(PlayerState newState)
    {
        DisableCurrentState();
        currentState = newState;
        EnableCurrentState();
    }

    private void DisableCurrentState()
    {
        switch (currentState)
        {
            case PlayerState.Recolection:
                recolectionScript.enabled = false;
                break;
            case PlayerState.Build:
                buildScript.enabled = false;
                break;
            case PlayerState.Combat:
                combatScript.enabled = false;
                break;
        }
    }

    private void EnableCurrentState()
    {
        switch (currentState)
        {
            case PlayerState.Recolection:
                recolectionScript.enabled = true;
                break;
            case PlayerState.Build:
                buildScript.enabled = true;
                break;
            case PlayerState.Combat:
                combatScript.enabled = true;
                break;
        }
    }

    public void OnMenuOpen(bool state)
    {
        recolectionScript.enabled = state;
        buildScript.enabled = state;
        combatScript.enabled = state;
    }
}
