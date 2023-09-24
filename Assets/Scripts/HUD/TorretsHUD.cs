using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TorretsHUD : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject Torrets;
    private Transform initialParent;
    private InteractiveWorld world;


    private void Start()
    {
        Debug.Log("esto es el padre "+this.transform.parent.gameObject);
        Debug.Log("hola");
        initialParent=this.transform.parent;
        world = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<TorretsManager>().IW;
        //world = GetComponent<InteractiveWorld>();
    }
    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerEventData = data as PointerEventData;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerEventData.position, canvas.worldCamera, out position);

        transform.position = canvas.transform.TransformPoint(position);

        HUD.gameObject.SetActive(false);
        this.transform.parent=canvas.transform;
        this.gameObject.SetActive(true);
    }
    public void DropHandler()
    {
        this.transform.parent = initialParent;
        transform.position = this.transform.parent.position;
        GameObject aux = (GameObject)Instantiate(Torrets, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log(aux);
        world.Construct(aux);
    }

}