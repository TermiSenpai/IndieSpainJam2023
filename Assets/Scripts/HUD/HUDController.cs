using UnityEngine;

public class HUDController : MonoBehaviour
{
    private GameObject hud; // Reference to the HUD object
    public bool IsHUDActive { get; private set; } // Current HUD state

    private void Start()
    {
        hud = gameObject; // Reference to the HUD GameObject
        DeactivateHUD(); // Deactivate the HUD on start
    }

    // Method to activate the HUD
    public void ActivateHUD()
    {
        hud.SetActive(true); // Activate the HUD
        IsHUDActive = true; // Update the HUD state
        // You can add any additional logic you need when activating the HUD here
    }

    // Method to deactivate the HUD
    public void DeactivateHUD()
    {
        hud.SetActive(false); // Deactivate the HUD
        IsHUDActive = false; // Update the HUD state
        // You can add any additional logic you need when deactivating the HUD here
    }

    // Method to activate the HUD (can be called by other scripts if needed)
    public void HUDActivated()
    {
        ActivateHUD();
    }

    // Method to deactivate the HUD (can be called by other scripts if needed)
    public void HUDDeactivated()
    {
        DeactivateHUD();
    }
}
