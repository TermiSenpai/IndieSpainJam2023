using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TorretsHUD : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject HUD;
    [SerializeField] private TurretStats turretStats;
    [SerializeField] private PlayerStats player;
    private Transform initialParent;
    public InteractiveWorld world;


   

    private void Start()
    {
        initialParent=this.transform.parent;
    }
    public void DragHandler(BaseEventData data)
    {
        ///TODO :mirar si puedes construir si tienes mana/madera 

        if (player.currentMagic <= turretStats.buildCost) return;


        ///TODO mirar a ver si se puede construir
        /*if (!PlayerStateManager.Instance.canBuild)
        {
            return;
        }*/

        this.GetComponent<Image>().color = new Color32(255, 255, 255, 50);
        PointerEventData pointerEventData = data as PointerEventData;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerEventData.position, canvas.worldCamera, out position);

        transform.position = canvas.transform.TransformPoint(position);

        HUD.gameObject.SetActive(false);
        this.transform.SetParent(canvas.transform);
        this.gameObject.SetActive(true);
    }
    public void DropHandler()
    {
        this.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        this.transform.SetParent(initialParent);
        transform.position = this.transform.parent.position;
        GameObject aux = (GameObject)Instantiate(turretStats.turretGameobject, new Vector3(0, 0, 0), Quaternion.identity);
        world.Construct(aux);

        player.currentMagic -= turretStats.buildCost;
        
    }

}
