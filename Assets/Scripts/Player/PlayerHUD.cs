using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            Debug.Log(HUD.activeSelf);
            if (!HUD.activeSelf) {
                HUD.SetActive(true);
            }
            else
            {
                HUD.SetActive(false);
            }
            Debug.Log("tab pressed");
        }
    }
}
