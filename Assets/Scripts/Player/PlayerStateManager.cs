using UnityEngine;

// Enum to represent the different states of the player
public enum PlayerState
{
    Recolection, // State for recolection phase
    Build,       // State for build phase
    Combat       // State for combat phase
}

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;

    // References to the scripts controlling different player states
    [SerializeField] private MonoBehaviour recolectionScript;
    [SerializeField] private MonoBehaviour buildScript;
    [SerializeField] private MonoBehaviour combatScript;

    private PlayerState currentState = PlayerState.Recolection; // Current state of the player

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        // Subscribing to events triggered by DayCycle for different times of the day
        DayCycle.DayStartRelease += OnStartDay;
        DayCycle.EveningStartRelease += OnStartEvening;
        DayCycle.NightStartRelease += OnStartNight;
    }

    private void OnDisable()
    {
        // Unsubscribing from events when the object is disabled or destroyed
        DayCycle.DayStartRelease -= OnStartDay;
        DayCycle.EveningStartRelease -= OnStartEvening;
        DayCycle.NightStartRelease -= OnStartNight;
    }

    // Event handlers for different times of the day
    private void OnStartDay()
    {
        // Switching to recolection phase
        SwitchState(PlayerState.Recolection);
    }

    private void OnStartEvening()
    {
        // Switching to build phase
        SwitchState(PlayerState.Build);
    }

    private void OnStartNight()
    {
        // Switching to combat phase
        SwitchState(PlayerState.Combat);
    }

    // Method to switch between player states
    public void SwitchState(PlayerState newState)
    {
        // Disabling current state before switching
        DisableCurrentState();
        currentState = newState;
        // Enabling new state after switching
        EnableCurrentState();
    }

    // Method to disable the current player state
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

    // Method to enable the current player state
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

    // Method to toggle all player states on or off (used for menu open/close)
    public void OnMenuOpen(bool state)
    {
        recolectionScript.enabled = state;
        buildScript.enabled = state;
        combatScript.enabled = state;
    }
}
