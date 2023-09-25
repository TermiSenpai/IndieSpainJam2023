using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;
    [SerializeField] private MonoBehaviour combatScript;
     public bool canBuild =true;
     public bool canRecolection =true;


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
        // Start recolection phase
        //recolectionScript.enabled = true;
        combatScript.enabled = false;
        //buildScript.enabled = false;
    }

    private void OnStartEvening()
    {
        //recolectionScript.enabled = false;
        combatScript.enabled = false;
        // Start build phase
        //buildScript.enabled = true;
    }

    private void OnStartNight()
    {
//recolectionScript.enabled = false;
        // Start combat phase
        combatScript.enabled = true;
        //buildScript.enabled = false;
    }

    public void OnMenuOpen(bool state)
    {
        combatScript.enabled = state;
        //recolectionScript.enabled = state;
       // buildScript.enabled = state;
    }

}
