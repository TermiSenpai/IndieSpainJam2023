using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    private HUD hud;
    [SerializeField] KeyCode keycode;

    private void Start()
    {
        hud = HUD.GetComponent<HUD>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            if (!HUD.activeSelf) {
                Activate();
            }
            else
            {
                DeActivate();
            }
        }
    }

    public void Activate()
    {
        hud.HUDActivated();
    }

    public void DeActivate()
    {
        hud.HUDDeactivated();
    }
}
