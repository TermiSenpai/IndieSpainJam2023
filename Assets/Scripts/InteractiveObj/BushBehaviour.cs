using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class BushBehaviour : MonoBehaviour
{
    private bool mousePressed = false;
    private bool mouseOver = false;
    private float count = 0;
    [SerializeField] private float timer = 2;
    [SerializeField] private GameObject Hunger;
    private bool clickeable=true;

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
