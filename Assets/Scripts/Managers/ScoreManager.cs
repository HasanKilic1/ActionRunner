using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI scoreText;
    private int currentScore;
    public void AddScore(int score)
    {
        if (gameManager.GameOver) return;
        currentScore += score;
        scoreText.text = "Score " + currentScore.ToString();
    }
}
