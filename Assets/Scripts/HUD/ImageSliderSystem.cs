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
    [SerializeField] Image firstWood;
    [SerializeField] Image secondWood;
    [SerializeField] Image thirdWood;

    [Header("Sprites")]
    [SerializeField] Sprite woodZero;
    [SerializeField] Sprite woodOne;
    [SerializeField] Sprite woodTwo;
    [SerializeField] Sprite woodMin;
    [SerializeField] Sprite woodMid;
    [SerializeField] Sprite woodMax;


    [Header("Player")]
    [SerializeField] PlayerStats stats;

    private void Start()
    {
        RestartResources();
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerTakeDamageRelease += OnHealthUpdate;
        BushBehaviour.OnMagicRelease += OnMagicUpdate;
        BushBehaviour.OnBushRelease += OnFoodUpdate;
        TreeBehaviour.OnTreeReleased += OnWoodUpdate;

        DayCycle.DayStartRelease += RestartResources;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerTakeDamageRelease -= OnHealthUpdate;
        BushBehaviour.OnMagicRelease -= OnMagicUpdate;
        BushBehaviour.OnBushRelease -= OnFoodUpdate;
        TreeBehaviour.OnTreeReleased -= OnWoodUpdate;

        DayCycle.DayStartRelease -= RestartResources;
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
                firstWood.sprite = woodZero;
                secondWood.sprite = woodZero;
                thirdWood.sprite = woodZero;
                break;
            case 1:
                firstWood.sprite = woodOne;
                break;
            case 2:
                firstWood.sprite = woodTwo;
                break;
            case 3:
                firstWood.sprite = woodMin;
                break;
            case 4:
                secondWood.sprite = woodOne;
                break;
            case 5:
                secondWood.sprite = woodTwo;
                break;
            case 6:
                secondWood.sprite = woodMid;
                break;
            case 7:
                thirdWood.sprite = woodOne;
                break;
            case 8:
                thirdWood.sprite = woodTwo;
                break;
            case 9:
                thirdWood.sprite = woodMax;
                break;
        }
    }

    void RestartResources()
    {
        stats.currentFood = 0;
        stats.currentWood = 0;
        stats.currentMagic = 0;

        OnFoodUpdate();
        OnMagicUpdate();
        OnWoodUpdate();
    }

}
