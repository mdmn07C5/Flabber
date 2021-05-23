using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] static Score instance;
    [SerializeField] TextMeshProUGUI inGameScoreText;
    [SerializeField] TextMeshProUGUI gameOverScoreText;
    [SerializeField] TextMeshProUGUI gameOverHighScore;
    [SerializeField] Sprite goldMedal;
    [SerializeField] Sprite silverMedal;
    [SerializeField] Sprite bronzeMedal;
    [SerializeField] Sprite participationMedal;
    [SerializeField] UnityEngine.UI.Image medal;

    int score = 0;

    void Awake()
    {
        if (null != instance && this != instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        // ScoreTrigger.OnPlayerClear.AddListener(HandleOnPlayerClear);
        // Kill.OnPlayerCollision.AddListener(HandleOnPlayerCollision);
        GameManager.OnPlayerScore.AddListener(HandleOnPlayerScore);
        GameManager.OnPlayerKill.AddListener(HandleOnPlayerKill);
        GameManager.OnPauseToggled.AddListener(HandleOnPauseToggled);
    }

    void HandleOnPlayerScore(int points)
    {
        score += points;
        inGameScoreText.text = score + "";
    }

    void HandleOnPlayerKill()
    {
        UpdateScore();
        score = 0;
        inGameScoreText.text = score + "";
    }

    void HandleOnPauseToggled()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        // int highScore =  PlayerPrefs.GetInt("HighScore", 0);
        // gameOverScoreText.text = score + "";
        // gameOverHighScore.text = highScore + "";
        // PlayerPrefs.SetInt("HighScore", Mathf.Max(score, highScore));

        int goldScore = PlayerPrefs.GetInt("GoldScore", 0);
        int silverScore = PlayerPrefs.GetInt("SilverScore", 0);
        int bronzeScore = PlayerPrefs.GetInt("BronzeScore", 0);
        gameOverScoreText.text = score + "";

        if ( score >= goldScore )
        {
            PlayerPrefs.SetInt("GoldScore", score);
            if ( score > goldScore )
            {
                PlayerPrefs.SetInt("SilverScore", goldScore);
                PlayerPrefs.SetInt("BronzeScore", silverScore);
            }
            
            medal.sprite = goldMedal;
        }
        else if ( score >= silverScore )
        {
            PlayerPrefs.SetInt("SilverScore", score);
            if ( score > silverScore )
            {
                PlayerPrefs.SetInt("BronzeScore", silverScore);
            }
            medal.sprite = silverMedal;
        }
        else if ( score >= bronzeScore )
        {
            PlayerPrefs.SetInt("BronzeScore", score);
            medal.sprite = bronzeMedal;
        }
        else 
        {
            medal.sprite = participationMedal;
        }
        
        // gameOverHighScore.text = goldScore + ""; // previous best, not current
        gameOverHighScore.text = PlayerPrefs.GetInt("GoldScore", 0) +""; // best so far
    }
}
