using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TorretsHUD : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject HUD;
    private Transform initialParent;


    private void Awake()
    {
        initialParent=this.transform.parent;
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
    }

}
