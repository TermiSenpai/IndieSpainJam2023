using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    private HUD hud;
    [SerializeField] KeyCode keycode;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip abrirLibro;
    [SerializeField] AudioClip cerrarLibro;

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
                audioSource.PlayOneShot(abrirLibro);
            }
            else
            {
                DeActivate();
                audioSource.PlayOneShot(cerrarLibro);
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
