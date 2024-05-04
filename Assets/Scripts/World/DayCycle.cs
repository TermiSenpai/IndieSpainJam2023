using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering; // used to access the volume component
using UnityEngine.Tilemaps;
using System;


//Day cicle enum
public enum DayTime
{
    Day = 0,
    Evening = 1,
    Night = 2,
    Sunrise = 3
}

public class DayCycle : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI timeDisplay;
    //[SerializeField] private TextMeshProUGUI dayDiplay;

    
    //Properties
    private int time = 60;
    private int days = 0;
    private float seconds = 0;
    private bool activateLights;
    private bool canCampfiresBeOn = true;


    //References
    [SerializeField] private Volume ppv; //post processing volume
    [SerializeField] int DayTimer = 5;
    [SerializeField] int NightTimer = 9;
    [SerializeField] int SunriseTimer = 5;
    [SerializeField] int EveningTimer = 5;
    [SerializeField] private GameObject[] lights;
    [SerializeField] private DayTime DTime = DayTime.Day;
    [SerializeField] private Tilemap m_Tilemap = null;
    [SerializeField] private TileBase TBase = null;
    [SerializeField] private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private List<Tuple<Vector3Int, string>> TilesDates; //Tiles for the days

    //Delegates
    public delegate void DayCycleDelegate();
    public static DayCycleDelegate DayStartRelease;
    public static DayCycleDelegate EveningStartRelease;
    public static DayCycleDelegate NightStartRelease;
    public static DayCycleDelegate SunriseStartRelease;
    public delegate void GameClearDelegate();
    public static GameClearDelegate GameClearRelease;

    //Control Properties
    [HideInInspector]
    public bool gameStarted = false;


    private void OnEnable()
    {
        Campfire.OnNoCampfireRelease += onCampfireoff;
    }

    private void OnDisable()
    {
        Campfire.OnNoCampfireRelease -= onCampfireon;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Set references
        TilesDates = new List<Tuple<Vector3Int, string>>();
        List<Vector3> availablePlaces = new List<Vector3>();
        ppv = gameObject.GetComponent<Volume>();

        //Add each tile in the Dictionary to access the data
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
        //Get the posicions for the dates tiles
        for (int n = m_Tilemap.cellBounds.xMin; n < m_Tilemap.cellBounds.xMax; n++)
        {
            for (int p = m_Tilemap.cellBounds.yMin; p < m_Tilemap.cellBounds.yMax; p++)
            {
                Vector3Int pos = new Vector3Int(n, p, 0);
                TileBase tile = m_Tilemap.GetTile(pos);
                if (tile == null) { }
                else
                {
                    if (dataFromTiles.ContainsKey(tile))
                    {
                        TilesDates.Add(new Tuple<Vector3Int, string>(pos, tile.name));
                    }
                }
            }
        }
        //Set the new DateTiles in their position
        foreach (Tuple<Vector3Int, string> t in TilesDates)
        {
            m_Tilemap.SetTile(t.Item1, TBase);
        }

        //Start the code
        DayChange();
        DayStartRelease?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted) return;
        CalcTime();
    }

    //Calculate the time of the day
    public void CalcTime()
    {
        seconds += Time.deltaTime;

        if (seconds >= time)
        {
            seconds = 0;
            ///TODO: Switch?
            if (((int)DTime + 1) < 4)
            {
                DTime = (DayTime)((int)DTime + 1);
                if ((int)DTime == 1)
                {
                    EveningStartRelease?.Invoke();
                }
                else if ((int)DTime == 2)
                {
                    NightStartRelease?.Invoke();
                }
                else if ((int)DTime == 3)
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
        ///TODO: Switch
        if (DTime == DayTime.Evening)
        {
            time = EveningTimer;
        }
        else if (DTime == DayTime.Sunrise)
        {
            time = SunriseTimer;
        }
        else if (DTime == DayTime.Day)
        {
            time = DayTimer;
            canCampfiresBeOn = true;
        }
        else
        {
            time = NightTimer;
        }

        ControlPPV();
    }

    //Set light intensity
    public void ControlPPV()
    {
        if (DTime == DayTime.Evening)
        {
            ppv.weight = seconds / time;
            if (activateLights == false)
            {
                if (canCampfiresBeOn)
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
        }

        if (DTime == DayTime.Sunrise)
        {
            ppv.weight = 1 - (seconds / time);
            if (activateLights == true)
            {
                if (seconds > (time / 2))
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

    /// TODO: day 1 tile?
    //Set a new tile for each day
    private void DayChange()
    {

        if (days >= 5)
        {
            GameClearRelease?.Invoke();
            return;
        }
        m_Tilemap.SetTile(TilesDates[days].Item1, Resources.Load<TileBase>("Tiles/" + TilesDates[days].Item2));
        days++;
    }

    void onCampfireon()
    {
        canCampfiresBeOn = true;
    }

    void onCampfireoff()
    {
        canCampfiresBeOn = false;
    }
}
