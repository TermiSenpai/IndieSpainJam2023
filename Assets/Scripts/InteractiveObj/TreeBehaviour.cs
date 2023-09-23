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
    //[SerializeField] private TreeType treeType = TreeType.Arbol_1;

    private void Start()
    {
        /*SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>(treeType.ToString());
        Debug.Log(treeType.ToString());*/
    }

    void Update()
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
            Debug.Log("Clicking");
            count += Time.deltaTime;

        }

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
