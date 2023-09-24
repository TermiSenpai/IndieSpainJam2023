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
    private Vector3 posIni;
    [SerializeField] private float timer = 5;

    float speed = 1.0f; //how fast it shakes
    float amount = 0.1f; //how much it shakes
    //[SerializeField] private TreeType treeType = TreeType.Arbol_1;

    private void Start()
    {
        /*SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>(treeType.ToString());
        Debug.Log(treeType.ToString());*/
    }

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
            //transform.position= new Vector2(transform.position.x+(Mathf.Sin(Time.time * speed) * amount),transform.position.y);
        }

    }
    IEnumerator Tremble()
    {
        //for (int i = 0; i < 10; i++)
       // {
            Debug.Log("Clicking");
            count += Time.deltaTime;
            transform.localPosition += new Vector3(0.1f, 0, 0);
            yield return new WaitForSeconds(0.01f);
            transform.localPosition -= new Vector3(0.1f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        //}
    }

    void OnMouseDown()
    {
        mousePressed = true;
        Debug.Log("Sprite Clicked");
        
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
