using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Action<int> onScoreChange;
    private int score;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    public void AddScore(int addAmount)
    {
        score += addAmount;
        onScoreChange?.Invoke(score);
    }

}
