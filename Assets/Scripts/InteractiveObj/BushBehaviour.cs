using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractiveType
{
    Magic,
    Bush
}



public class BushBehaviour : MonoBehaviour
{
    private bool mousePressed = false;
    private bool mouseOver = false;
    private float count = 0;
    [SerializeField] private InteractiveType interactiveType = InteractiveType.Bush;
    [SerializeField] private float timer = 2;
    [SerializeField] private GameObject Hunger;
    private bool clickeable = true;

    public delegate void InteractiveDelegate();
    public static InteractiveDelegate OnMagicRelease;
    public static InteractiveDelegate OnBushRelease;

    [SerializeField] PlayerStats player;

    void FixedUpdate()
    {
        ///TODO: RECOLECTABLE?


        if (clickeable)
        {
            if ((!mouseOver) || (!mousePressed))
            {
                count = 0;
                return;
            }
            else
            if (count >= timer)
            {
                clickeable = false;
                Hunger.SetActive(false);
                switch (interactiveType)
                {
                    case InteractiveType.Magic:

                        if (player.currentMagic >= player.maxMagicQuantity)
                        {
                            player.currentMagic = player.maxMagicQuantity;
                            break;
                        }
                        player.currentMagic += 1;

                        OnMagicRelease?.Invoke();
                        break;

                    case InteractiveType.Bush:
                        if (player.currentFood >= player.maxFoodQuantity)
                        {
                            player.currentFood = player.maxFoodQuantity;
                            break;
                        }
                        player.currentFood += 1;

                        OnBushRelease?.Invoke();
                        break;
                }
                //Debug.Log("Donete");
            }
            else
            {

                StartCoroutine(Tremble());
            }
        }

    }
    IEnumerator Tremble()
    {
        count += Time.deltaTime;
        transform.localPosition += new Vector3(0.1f, 0, 0);
        yield return new WaitForSeconds(0.01f);
        transform.localPosition -= new Vector3(0.1f, 0, 0);
        yield return new WaitForSeconds(0.01f);
    }

    void OnMouseDown()
    {
        mousePressed = true;
    }

    private void OnMouseOver()
    {
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        mouseOver = false;
    }

    void OnMouseUp()
    {
        mousePressed = false;
    }


    private void OnEnable()
    {
        DayCycle.DayStartRelease += RestartBush;
    }

    private void OnDisable()
    {
        DayCycle.DayStartRelease -= RestartBush;        
    }

    void RestartBush()
    {
        Hunger.SetActive(true);
    }

}
