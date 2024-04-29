using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    public TMP_Dropdown windowDropdown; // Asegúrate de tener el TextMeshPro Dropdown asignado desde el Inspector
    private const string PlayerPrefsKey = "ModoVentana";

    private void Start()
    {
        // Agregar opciones al Dropdown
        windowDropdown.ClearOptions();
        windowDropdown.AddOptions(new List<string> { "Ventana", "Pantalla completa", "Ventana sin bordes" });

        // Recuperar la última configuración del modo de ventana
        int modoVentanaGuardado = PlayerPrefs.GetInt(PlayerPrefsKey, 0);
        windowDropdown.value = modoVentanaGuardado;

        // Suscribirse al evento de cambio del Dropdown
        windowDropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        // Configurar el modo de ventana al valor guardado
        OnDropdownValueChanged(modoVentanaGuardado);
    }

    private void OnDropdownValueChanged(int value)
    {
        // Guardar la configuración del modo de ventana seleccionada
        PlayerPrefs.SetInt(PlayerPrefsKey, value);
        PlayerPrefs.Save();

        // Configurar el modo de ventana
        SetModoVentana(value);
    }

    private void SetModoVentana(int modoVentana)
    {
        switch (modoVentana)
        {
            case 0: // Ventana
                Screen.SetResolution(800, 600, false);
                break;
            case 1: // Pantalla completa
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                break;
            case 2: // Ventana sin bordes (exclusive fullscreen)
                
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                break;
        }
    }
}
