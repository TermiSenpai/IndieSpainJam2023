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
    [SerializeField] private InteractiveType interactiveType=InteractiveType.Bush;
    [SerializeField] private float timer = 2;
    [SerializeField] private GameObject Hunger;
    private bool clickeable=true;

    public delegate void InteractiveDelegate();
    public static InteractiveDelegate onMagicRelease;
    public static InteractiveDelegate onBushRelease;

    void FixedUpdate()
    {
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
                Hunger.gameObject.SetActive(false);
                switch (interactiveType)
                {
                    case InteractiveType.Magic:
                        onMagicRelease?.Invoke();
                        break;
                        
                    case InteractiveType.Bush:
                        onBushRelease?.Invoke();
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
        mouseOver=false;
    }

    void OnMouseUp()
    {
        mousePressed=false;
    }


}
