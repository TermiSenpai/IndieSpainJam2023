using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TorretsHUD : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private bool isMoving;
    private GameObject dupe;
    private Vector3 offset;
    private Vector3 initialPlace;

    private void Awake()
    {
        initialPlace = transform.position - canvas.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            dupe.transform.position = (Camera.main.ScreenToWorldPoint(Input.mousePosition)+offset);
        }
    }
    public void MouseDown()
    {
        Debug.Log("Click");
        isMoving = true;
        offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dupe = GameObject.Instantiate(this.gameObject);
        dupe.transform.parent = this.transform.parent.gameObject.transform;
    }

    public void MouseUp()
    {
        isMoving = false;
        Destroy(dupe);
    }

    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerEventData = data as PointerEventData;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerEventData.position, canvas.worldCamera, out position);

        transform.position = canvas.transform.TransformPoint(position);

    }
    public void DropHandler()
    {
        transform.position = this.transform.parent.position;
    }

}
