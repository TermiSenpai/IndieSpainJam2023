using Cinemachine;
using System.Collections;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float centerSpeed;
    [SerializeField] CinemachineFramingTransposer framingTransposer;

    private void OnEnable()
    {
        PlayerHealth.PlayerDeathRelease += OnPlayerDeath;
    }
    private void OnDisable()
    {
        PlayerHealth.PlayerDeathRelease -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        CenterCamera();
        StartCoroutine(CameraZoom());
    }

    void CenterCamera()
    {

    }

    private IEnumerator CameraZoom()
    {
        // Obtiene el componente CinemachineFramingTransposer de la Virtual Camera.
        framingTransposer = playerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        while (playerCamera.m_Lens.OrthographicSize > 2)
        {
            playerCamera.m_Lens.OrthographicSize -= Time.deltaTime * zoomSpeed;

            if (framingTransposer != null && framingTransposer.m_DeadZoneHeight > 0)
            {
                // Centra la cámara en el personaje.

                framingTransposer.m_DeadZoneWidth -= Time.deltaTime;
                framingTransposer.m_DeadZoneHeight -= Time.deltaTime;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
