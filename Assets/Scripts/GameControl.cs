using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;

    public Text title;
    public Text howToPlay;
    public bool gameReady = true;

    public Text gameOverText;
    public bool gameIsOver = false;
    public bool gameStarted = false;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Time.timeScale = 0;
        EnableTitle();
        EnableInstructions();
        DisableGameOverText();
    }

    public void EnableTitle()
    {
        title.enabled = true;
    }

    public void DisableTitle()
    {
        title.enabled = false;
    }

    public void EnableInstructions()
    {
        howToPlay.enabled = true;
    }

    public void DisableInstructions()
    {
        howToPlay.enabled = false;
    }

    public void EnableGameOverText()
    {
        gameOverText.enabled = true;
    }

    public void DisableGameOverText()
    {
        gameOverText.enabled = false;
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        DisableTitle();
        DisableInstructions();
        gameReady = true;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverText.enabled = true;
        gameIsOver = true;
        audioSource.Stop();
    }

    private void Update()
    {
        if (gameReady && !gameStarted && Input.GetButtonDown("Horizontal"))
        {
            Time.timeScale = 1;
            DisableTitle();
            DisableInstructions();
            DisableGameOverText();
            audioSource.Play();
            gameStarted = true;
        }

        if (gameIsOver && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Game");
        }
    }
}
