using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioSource m_AudioSource;
    [SerializeField]private AudioClip m_ClipDay;
    [SerializeField]private AudioClip m_ClipNight;
    [SerializeField]private AudioClip m_ClipTwightlight;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = this.GetComponent<AudioSource>();
        m_AudioSource.loop = true;
    }

    private void OnEnable()
    {
        DayCycle.DayStartRelease += OnDayStarts;
        DayCycle.EveningStartRelease += onTwilightStarts;
        DayCycle.NightStartRelease += OnNightStarts;
        DayCycle.SunriseStartRelease += onTwilightStarts;
}

    private void OnDisable()
    {
        
    }


    private void OnDayStarts()
    {
        m_AudioSource.clip = m_ClipDay;
        m_AudioSource.Play();
    }
    private void OnNightStarts()
    {
        m_AudioSource.clip = m_ClipNight;
        m_AudioSource.Play();
    }

    private void onTwilightStarts()
    {
        m_AudioSource.clip = m_ClipTwightlight;
        m_AudioSource.Play();
    }


}
