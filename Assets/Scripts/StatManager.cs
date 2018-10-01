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

    public Text adultStats;
    public int totalAdultThingsDone = 0;
    public float doctorAppointmentsMade = 0;
    public float taxReturnFormsFilled = 0;
    public float beersDrank = 0;

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
    }

    private void Start()
    {
        currentHealth = initHealth;
        currentMoney = initMoney;
        currentMorale = initMorale;

        adultStats.text =
            "Your body is made of:\n" +
            "0% Medications\n" +
            "0% Tax Returns\n" +
            "0% Beer";
    }

    private void FixedUpdate()
	{
		float val = statDecrement * Time.deltaTime;
		currentHealth -= val;
		currentMoney -= val;
		currentMorale -= val;
		UpdateSlider(Stat.Health);
		UpdateSlider(Stat.Money);
		UpdateSlider(Stat.Morale);
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

    public void UpdateCounts(BlockScript.BlockType blockType)
    {
        switch (blockType)
        {
            case BlockScript.BlockType.Prescriptions:
                doctorAppointmentsMade++;
                break;
            case BlockScript.BlockType.TaxReturn:
                taxReturnFormsFilled++;
                break;
            case BlockScript.BlockType.Beer:
                beersDrank++;
                break;
        }
        totalAdultThingsDone++;

        float percentDoctors =
            (doctorAppointmentsMade / totalAdultThingsDone) * 100.0f;
        float percentTaxes =
            (taxReturnFormsFilled / totalAdultThingsDone) * 100.0f;
        float percentBeer =
            (beersDrank / totalAdultThingsDone) * 100.0f;
        adultStats.text =
            "Your life is made of:\n" +
            percentDoctors.ToString("0") +
            "% Medications\n" +
            percentTaxes.ToString("0") +
            "% Tax Returns\n" +
            percentBeer.ToString("0") +
            "% Beer\n";
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
                GameControl.instance.GameOver();
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
                GameControl.instance.GameOver();
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
                GameControl.instance.GameOver();
            }
        }
		UpdateSlider(Stat.Morale);
	}
}
