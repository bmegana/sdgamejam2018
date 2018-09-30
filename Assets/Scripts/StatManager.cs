using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public Text gameOverText;
    private bool gameOver = false;

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
        gameOverText.enabled = false;
    }

    private void Start()
    {
        currentHealth = initHealth;
        currentMoney = initMoney;
        currentMorale = initMorale;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        gameOverText.enabled = true;
        gameOver = true;
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
                GameOver();
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
                GameOver();
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
                GameOver();
            }
        }
        moraleSlider.value = currentMorale / maxMorale;
    }

    private void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Game");
        }
    }
}
