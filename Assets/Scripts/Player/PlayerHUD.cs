using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private KeyCode toggleKey;
    [SerializeField] private HUDController hudController; // Reference to the HUDController script
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openBookSound;
    [SerializeField] private AudioClip closeBookSound;

    // Update is called once per frame
    private void Update()
    {
        // Check if the toggle key is pressed
        if (Input.GetKeyDown(toggleKey))
        {
            // Check if the HUD is not active
            if (!hudController.IsHUDActive)
            {
                // Activate the HUD
                ActivateHUD();
            }
            else
            {
                // Deactivate the HUD
                DeactivateHUD();
            }
        }
    }

    // Method to activate the HUD
    private void ActivateHUD()
    {
        // Activate the HUD using the HUDController
        hudController.ActivateHUD();
        // Play the open book sound
        PlaySound(openBookSound);
    }

    // Method to deactivate the HUD
    private void DeactivateHUD()
    {
        // Deactivate the HUD using the HUDController
        hudController.DeactivateHUD();
        // Play the close book sound
        PlaySound(closeBookSound);
    }

    // Method to play a sound clip
    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
