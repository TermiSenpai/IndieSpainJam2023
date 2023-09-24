using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{

    [SerializeField] PlayerStateManager state;
    [SerializeField] GameObject TorretsHUD;
    [SerializeField] GameObject OptionHUD;
    // Start is called before the first frame update
    
    public void HUDActivated()
    {
        this.gameObject.SetActive(true);
        TorretsHUD.SetActive(true);
        OptionHUD.SetActive(false);
        state.OnMenuOpen(false);
    }

    public void HUDDeactivated()
    {
        TorretsHUD.SetActive(false);
        OptionHUD.SetActive(false);
        state.OnMenuOpen(true);
        this.gameObject.SetActive(false);
    }
}
