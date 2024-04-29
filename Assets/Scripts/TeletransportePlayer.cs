using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeletransportePlayer : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] GameObject m_PlayerPrefab;
    [SerializeField] Transform m_Camp;

    void Start()
    {
        
    }

    public void Campamento()
    {
        m_PlayerPrefab.transform.position = m_Camp.transform.position;

    }
}
