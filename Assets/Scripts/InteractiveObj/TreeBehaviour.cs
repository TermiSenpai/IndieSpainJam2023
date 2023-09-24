using System.Collections;
using UnityEngine;

public enum TreeType
{
    Arbol_1 , 
    Arbol_2 , 
    Arbol_3
}

public class TreeBehaviour : MonoBehaviour
{
    private bool mousePressed = false;
    private bool mouseOver = false;
    private float count = 0;
    [SerializeField] private float timer = 5;

    void FixedUpdate()
    {
        if ((!mouseOver) || (!mousePressed))
        {
            count = 0;
            return; 
        }else
        if (count >= timer)
        {
            this.gameObject.SetActive(false);
            Debug.Log("Donete");
        }
        else
        {
            
            StartCoroutine(Tremble());
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
