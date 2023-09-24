using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSaves : MonoBehaviour
{
    [SerializeField]
    private bool clearPlayerPrefsInEditor = true; // Una bandera para habilitar o deshabilitar la eliminación de PlayerPrefs en el editor.

    private void Awake()
    {
        if (clearPlayerPrefsInEditor)
        {
#if UNITY_EDITOR
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs eliminadas en el editor.");
#endif
        }
    }
}
