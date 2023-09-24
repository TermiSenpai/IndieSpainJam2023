using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            HUD.SetActive(true);
            Debug.Log("tab pressed");
        }
    }
}
