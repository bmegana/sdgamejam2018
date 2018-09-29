using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public static StatManager instance;

    public float initHealth;
    private float currentHealth;
    public Slider healthSlider;

    public float initMoney;
    private float currentMoney;
    public Slider moneySlider;

    public float initMorale;
    private float currentMorale;
    public Slider moraleSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        currentHealth = initHealth;
    }

    public void IncreaseHealth(float value)
    {
        currentHealth += value;
        healthSlider.value = currentHealth / initHealth;
    }

    public void DecreaseHealth(float value)
    {
        currentHealth -= value;
        healthSlider.value = currentHealth / initHealth;
    }

    public void IncreaseMoney(float value)
    {
        currentMoney += value;
        moneySlider.value = currentMoney / initMoney;
    }

    public void DecreaseMoney(float value)
    {
        currentMoney -= value;
        moneySlider.value = currentMoney / initMoney;
    }

    public void IncreaseMorale(float value)
    {
        currentMorale += value;
        moraleSlider.value = currentMorale / initMorale;
    }

    public void DecreaseMorale(float value)
    {
        currentMorale -= value;
        moraleSlider.value = currentMorale / initMorale;
    }
}
