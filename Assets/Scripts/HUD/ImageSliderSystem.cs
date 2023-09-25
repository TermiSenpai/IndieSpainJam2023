using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageSliderSystem : MonoBehaviour
{
    [Header("Bars")]
    [SerializeField] Image healthBar;
    [SerializeField] Image magicBar;
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI foodTxt;
    [SerializeField] TextMeshProUGUI foodTxtColored;
    [Header("Wood boxes")]




    [Header("Player")]
    [SerializeField] PlayerStats stats;

    private void Start()
    {
        stats.currentFood = 0;
        stats.currentWood = 0;
        stats.currentMagic = 0;

        OnFoodUpdate();
        OnMagicUpdate();
        OnWoodUpdate();
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerTakeDamageRelease += OnHealthUpdate;
        BushBehaviour.OnMagicRelease += OnMagicUpdate;
        BushBehaviour.OnBushRelease += OnFoodUpdate;
        TreeBehaviour.OnTreeReleased += OnWoodUpdate;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerTakeDamageRelease -= OnHealthUpdate;
        BushBehaviour.OnMagicRelease -= OnMagicUpdate;
        BushBehaviour.OnBushRelease -= OnFoodUpdate;
        TreeBehaviour.OnTreeReleased -= OnWoodUpdate;

    }

    void OnHealthUpdate(float hp)
    {
        healthBar.fillAmount = hp / stats.MaxHealth;
    }

    void OnMagicUpdate()
    {
        magicBar.fillAmount = stats.currentMagic / stats.maxMagicQuantity;
    }

    void OnFoodUpdate()
    {
        foodTxt.text = $"x{stats.currentFood}";
        foodTxtColored.text = $"x{stats.currentFood}";
    }

    void OnWoodUpdate()
    {
        switch (stats.currentWood)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
        }
    }

}
