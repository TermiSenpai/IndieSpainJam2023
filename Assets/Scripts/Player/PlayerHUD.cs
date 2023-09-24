using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
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
        HUD.SetActive(true);
    }

    public void DeActivate()
    {
        HUD.SetActive(false);
    }
}
