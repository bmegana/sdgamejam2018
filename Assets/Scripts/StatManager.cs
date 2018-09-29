using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public static StatManager instance;

    public float initHealth;
    public float maxHealth;
    public float currentHealth;
    public Slider healthSlider;

    public float initMoney;
    public float maxMoney;
    public float currentMoney;
    public Slider moneySlider;

    public float initMorale;
    public float maxMorale;
    public float currentMorale;
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
        currentMoney = initMoney;
        currentMorale = initMorale;
    }

    public void IncreaseHealth(float value)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += value;
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        healthSlider.value = currentHealth / maxHealth;
    }

    public void DecreaseHealth(float value)
    {
        if (currentHealth > 0.0f)
        {
            currentHealth -= value;
            if (currentHealth <= 0.0f)
            {
                currentHealth = 0.0f;
            }
        }
        healthSlider.value = currentHealth / maxHealth;
    }

    public void IncreaseMoney(float value)
    {
        if (currentMoney < maxMoney)
        {
            currentMoney += value;
            if (currentMoney >= maxMoney)
            {
                currentMoney = maxMoney;
            }
        }
        moneySlider.value = currentMoney / maxMoney;
    }

    public void DecreaseMoney(float value)
    {
        if (currentMoney > 0.0f)
        {
            currentMoney -= value;
            if (currentMoney <= 0.0f)
            {
                currentMoney = 0.0f;
            }
        }
        moneySlider.value = currentMoney / maxMoney;
    }

    public void IncreaseMorale(float value)
    {
        if (currentMorale < maxMorale)
        {
            currentMorale += value;
            if (currentMorale >= maxMorale)
            {
                currentMorale = maxMorale;
            }
        }
        moraleSlider.value = currentMorale / maxMorale;
    }

    public void DecreaseMorale(float value)
    {
        if (currentMorale > 0.0f)
        {
            currentMorale -= value;
            if (currentMorale <= 0.0f)
            {
                currentMorale = 0.0f;
            }
        }
        moraleSlider.value = currentMorale / maxMorale;
    }
}
