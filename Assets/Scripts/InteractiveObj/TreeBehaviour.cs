using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TreeBehaviour : MonoBehaviour
{
    private bool mousePressed=false;
    private bool mouseOver = false;
    private float count = 0;
    float speed = 1.0f; //how fast it shakes
    float amount = 0.1f; //how much it shakes



    // Start is called before the first frame update
    void Start()
    {
   
        /* Tilemap tilemap = m_Tilemap;

         BoundsInt bounds = tilemap.cellBounds;
         TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

         for (int x = 0; x < bounds.size.x; x++)
         {
             for (int y = 0; y < bounds.size.y; y++)
             {
                 TileBase tile = allTiles[x + y * bounds.size.x];
                 if (tile != null)
                 {
                     Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                 }
                 else
                 {
                     Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                 }
             }
         }*/
        /*int rInt = Random.Range(0, dataFromObj.Count);
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = dataFromObj[rInt].sprite;*/
    }

    // Update is called once per frame
    void Update()
    {
        if(mouseOver && mousePressed)
        {
            if (count >= 5)
            {
                this.gameObject.SetActive(false);
                Debug.Log("Donete");
            }
            else
            {
                count += Time.deltaTime;
                //transform.position = new Vector3(Mathf.Sin(Time.time * speed) * amount, transform.position.y,transform.position.z);
                Debug.Log("Sigue");
            }
        }
        else
        {
            count = 0;
            Debug.Log("No more clicks");

        }
        /*if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    //Our custom method. 
                    CurrentClickedGameObject(raycastHit.transform.gameObject);
                }
            }
        }
        else
        {
            count = 0;
        }*/
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
    /*public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (gameObject.tag == "InteractiveObj")
        {
            Debug.Log("Hey");
            
        }
    }*/


}
