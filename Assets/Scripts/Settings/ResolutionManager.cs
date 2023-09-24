using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ResolutionManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown; // Asigna el componente Dropdown de TMPro desde el Inspector.

    private void Start()
    {
        // Obtener todas las resoluciones disponibles
        Resolution[] resolutions = Screen.resolutions;

        // Limpiar las opciones actuales del Dropdown
        resolutionDropdown.ClearOptions();

        // Crear una lista de cadenas para almacenar las resoluciones como texto
        List<string> resolutionOptions = new List<string>();

        // Agregar las resoluciones como opciones al Dropdown
        foreach (Resolution resolution in resolutions)
        {
            string option = resolution.width + "x" + resolution.height;
            resolutionOptions.Add(option);
        }

        resolutionDropdown.AddOptions(resolutionOptions);

        // Establecer la resolución actual como seleccionada en el Dropdown (puedes modificar esto según tus necesidades)
        int currentResolutionIndex = GetCurrentResolutionIndex(resolutions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Método para encontrar el índice de la resolución actual
    private int GetCurrentResolutionIndex(Resolution[] resolutions)
    {
        Resolution currentResolution = Screen.currentResolution;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentResolution.width && resolutions[i].height == currentResolution.height)
            {
                return i;
            }
        }
        return 0; // Si no se encuentra, se usa la primera resolución como predeterminada
    }

    // Método para cambiar la resolución según la selección del Dropdown
    public void SetResolution(int resolutionIndex)
    {
        Resolution[] resolutions = Screen.resolutions;
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution selectedResolution = resolutions[resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }
    }
}
