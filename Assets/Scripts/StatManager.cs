using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatManager : MonoBehaviour
{
    public static StatManager instance;

	private enum Stat
	{
		Health,
		Money,
		Morale
	}

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
	public float statDecrement = 1;

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

	private void Update()
	{
		float val = statDecrement * Time.deltaTime;
		currentHealth -= val;
		currentMoney -= val;
		currentMorale -= val;
		UpdateSlider(Stat.Health);
		UpdateSlider(Stat.Money);
		UpdateSlider(Stat.Morale);

		if (gameOver && Input.GetKeyDown(KeyCode.R))
		{
			Time.timeScale = 1;
			SceneManager.LoadScene("Game");
		}

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

	private void UpdateSlider(Stat stat)
	{
		switch (stat)
		{
			case Stat.Health:
				healthSlider.value = currentHealth / maxHealth;
				break;
			case Stat.Money:
				moneySlider.value = currentMoney / maxMoney;
				break;
			case Stat.Morale:
				moraleSlider.value = currentMorale / maxMorale;
				break;
		}
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
		UpdateSlider(Stat.Health);
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
		UpdateSlider(Stat.Health);
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
		UpdateSlider(Stat.Money);
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
		UpdateSlider(Stat.Money);
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
		UpdateSlider(Stat.Morale);
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
		UpdateSlider(Stat.Morale);
	}
}
