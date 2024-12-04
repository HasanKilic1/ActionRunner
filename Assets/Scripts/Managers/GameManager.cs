using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 5f;

    float timeLeft;
    private bool gameOver = false;
    public bool GameOver => gameOver;

    private void Start()
    {
        timeLeft = startTime;
        gameOverText.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartScene();
        }
        if (gameOver) return;

        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");
        if (timeLeft < 0)
        {
            FinishGame();
        }
    }

    public void IncreaseTimeLeft(float time)
    {
        timeLeft += time;
    }
    private void FinishGame()
    {
        gameOver = true;

        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
        
        playerController.enabled = false;
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
