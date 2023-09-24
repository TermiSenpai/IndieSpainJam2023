using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject settingsGroup;
    [SerializeField] GameObject creditsGroup;

    private GameObject currentMenu;


    public void OnGameStartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSettingsBtn()
    {
        ChangeCurrentGroup(settingsGroup);
    }

    public void OnCreditsBtn()
    {
        ChangeCurrentGroup(creditsGroup);
    }

    private void ChangeCurrentGroup(GameObject newGroup)
    {
        if (currentMenu != null && currentMenu != newGroup)
        {
            currentMenu.SetActive(false);
        }

        if (currentMenu == newGroup)
        {
            currentMenu.SetActive(false);
            currentMenu = null;
            return;
        }
        currentMenu = newGroup;
        currentMenu.SetActive(true);
    }

    public void OnExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
