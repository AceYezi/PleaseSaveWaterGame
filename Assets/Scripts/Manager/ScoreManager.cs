using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int currentScore;

    public event System.Action<int> OnScoreChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int score)
    {
        currentScore += score;
        OnScoreChanged?.Invoke(currentScore); // 触发事件
    }

    public void SubtractScore(int score)
    {
        currentScore -= score;
       // if (currentScore < 0) currentScore = 0;
        OnScoreChanged?.Invoke(currentScore); // 触发事件
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore); // 触发事件
    }

}
