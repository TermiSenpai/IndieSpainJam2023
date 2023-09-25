using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoreManager : MonoBehaviour
{
    [SerializeField] GameObject lore;
    [SerializeField] GameObject clear;
    [SerializeField] GameObject lose;

    [SerializeField] Animator blackScreen;

    [SerializeField] GameObject clickTxt;

    bool canExit = false;
    bool finished = false;
    [SerializeField] DayCycle daycycle;

    // Game start
    private void Start()
    {
        Invoke(nameof(EnableTxt), 2f);
    }

    void EnableTxt()
    {
        clickTxt.SetActive(true);
        canExit = true;
    }

    void DisableTxt()
    {
        clickTxt.SetActive(false);
    }


    // General
    public void OnClick()
    {
        if (!canExit) return;

        DisableTxt();
        lore.SetActive(false);
        daycycle.gameStarted = true;
    }

    private void OnEnable()
    {
        DayCycle.GameClearRelease += GameClear;
        PlayerHealth.PlayerDeathRelease += GameOver;
    }

    private void OnDisable()
    {
        DayCycle.GameClearRelease -= GameClear;
        PlayerHealth.PlayerDeathRelease -= GameOver;
    }

    private IEnumerator Fade(GameObject img)
    {
        finished = true;
        yield return new WaitForSeconds(5f);
        blackScreen.SetTrigger("In");
        yield return new WaitForSeconds(1);
        EnableImg(img);
        blackScreen.SetTrigger("Out");

        yield return new WaitForSeconds(5);
        ReturnToMenu();
    }

    void EnableImg(GameObject img)
    {
        img.SetActive(true);
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Game clear
    private void GameClear()
    {
        if (finished) return;
        StartCoroutine(Fade(clear));
    }

    // Game Over
    private void GameOver()
    {
        if (finished) return;
        StartCoroutine(Fade(lose));
    }
}
