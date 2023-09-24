using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; // using text mesh for the clock display

using UnityEngine.Rendering; // used to access the volume component
using UnityEngine.Tilemaps;
using System.Drawing;
using UnityEngine.Rendering.Universal;
using UnityEngine.WSA;
using System;

public enum DayTime
{
    Day=0,
    Evening=1,
    Night=2,
    Sunrise=3
}

public class DayCycle : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI timeDisplay;
    //[SerializeField] private TextMeshProUGUI dayDiplay;
    [SerializeField] private Volume ppv; //post processing volume

    private float seconds=0;
    [SerializeField] int DayTimer = 5;
    [SerializeField] int NightTimer = 9;
    [SerializeField] int SunriseTimer = 5;
    [SerializeField] int EveningTimer = 5;
    int time = 60;
    int days = 0;

    private bool activateLights;
    [SerializeField]private GameObject[] lights;
    private DayTime DTime = DayTime.Day;

    [SerializeField] private Tilemap m_Tilemap = null;
    [SerializeField] private TileBase TBase = null;
    [SerializeField] private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private List<Tuple<Vector3Int,string>> TilesDates;


    public delegate void DayCycleDelegate();
    public static DayCycleDelegate DayStartRelease;
    public static DayCycleDelegate EveningStartRelease;
    public static DayCycleDelegate NightStartRelease;
    public static DayCycleDelegate SunriseStartRelease;




    // Start is called before the first frame update
    void Start()
    {
        TilesDates = new List<Tuple<Vector3Int, string>>();

         dataFromTiles = new Dictionary<TileBase, TileData>();
        //Debug.Log("hey");
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
              //  Debug.Log(tile + ", " + tileData);
            }
        }

        List<Vector3> availablePlaces = new List<Vector3>();

        for (int n = m_Tilemap.cellBounds.xMin; n < m_Tilemap.cellBounds.xMax; n++)
        {
            for (int p = m_Tilemap.cellBounds.yMin; p < m_Tilemap.cellBounds.yMax; p++)
            {
                Vector3Int pos = new Vector3Int(n, p, 0);
                TileBase tile = m_Tilemap.GetTile(pos);
                //m_Tilemap.SetTile(new Vector3Int(n, p, 0),null);
                //Debug.Log(tile);
                if (tile == null) { }
                else
                {
                    if (dataFromTiles.ContainsKey(tile))
                    {
                        TilesDates.Add(new Tuple<Vector3Int, string>(pos,tile.name));
                    }
                }
            }
        }
        foreach(Tuple<Vector3Int, string> t in TilesDates)
        {
            m_Tilemap.SetTile(t.Item1,TBase);
        }
        DayChange();
        DayStartRelease?.Invoke();
        ppv = gameObject.GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        CalcTime();
        //DisplayTime();
    }

    public void CalcTime()
    {
        seconds += Time.deltaTime;
        //Debug.Log("paso "+ Time.fixedDeltaTime);

        if(seconds >= time)
        {
            seconds = 0;
            if (((int)DTime + 1) < 4)
            {
                DTime = (DayTime)((int)DTime + 1);
                if(((int)DTime + 1) == 1)
                {
                    EveningStartRelease?.Invoke();
                }else if(((int)DTime + 1) == 2)
                {
                    NightStartRelease?.Invoke();
                }else if(((int)DTime + 1) == 3)
                {
                    SunriseStartRelease?.Invoke();
                }
            }
            else
            {
                DayChange();
                DTime = (DayTime)0;
                DayStartRelease?.Invoke();
            }
        }
        if (DTime == DayTime.Evening)
        {
            time = EveningTimer;
        }
        else if(DTime == DayTime.Sunrise)
        {
            time = SunriseTimer;
        }
        else if(DTime == DayTime.Day)
        {
            time = DayTimer;
        }
        else
        {
            time = NightTimer;
        }

        ControlPPV();
    }

    public void ControlPPV()
    {
        if (DTime == DayTime.Evening)
        {
            ppv.weight = seconds / time;
            if (activateLights == false)
            {
                if (seconds > (time / 2))
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(true);
                    }
                    activateLights = true;
                }
            }
        }

        if (DTime == DayTime.Sunrise) {
            ppv.weight = 1 - (seconds / time);
            if (activateLights == true)
            {
                if (seconds > (time/2))
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(false);
                    }
                    activateLights = false;
                }
            }
        }
    }

    private void DayChange()
    {
        
        if (days >= 7) { return; }
        m_Tilemap.SetTile(TilesDates[days].Item1, Resources.Load<TileBase>("Tiles/"+ TilesDates[days].Item2));
        days++;
    }


    public void DisplayTime()
    {
        //timeDisplay.text = "Time: " + (int)seconds;
        //dayDiplay.text = "Day: " + days;
    }


}
