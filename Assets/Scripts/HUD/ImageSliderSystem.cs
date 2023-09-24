using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSliderSystem : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Image magicBar;

    [SerializeField] PlayerStats stats;



    private void OnEnable()
    {
        PlayerHealth.PlayerTakeDamageRelease += OnHealthUpdate;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerTakeDamageRelease -= OnHealthUpdate;

    }

    void OnHealthUpdate(float hp)
    {
        healthBar.fillAmount = hp / stats.MaxHealth;
    }

    void OnMagicUpdate(int magic)
    {
        magicBar.fillAmount = Mathf.Abs(magic / stats.maxMagicQuantity);
    }

}
