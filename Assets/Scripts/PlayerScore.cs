using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private float scoreIncreaseRate = 1f; // m/unit
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float speed = 6f;
    
    private int currentScore = 0;
    private int bestScore = 0;

    const float dt = 0.5f;

    private Coroutine scoreIncrease;

    void Awake()
    {
        scoreText.text = $"{currentScore}m";
        scoreIncrease = StartCoroutine(IncreaseScoreOverTime());
        bestScore = PlayerPrefs.GetInt("High score", bestScore);
    }

    IEnumerator IncreaseScoreOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(dt); // Fixed UI update rate
            currentScore += Mathf.RoundToInt(speed * scoreIncreaseRate);
            // currentScore += speed (m/s) * units_per_meter (u/m) * dt (s); increase in speed increases rate of score increase
            scoreText.text = $"{currentScore}m";
        }
    }

    void UpdateBestScore()
    {
        if (currentScore > bestScore)
        { 
            bestScore = currentScore;
            PlayerPrefs.SetInt("High score", bestScore);
        }
    }
}
