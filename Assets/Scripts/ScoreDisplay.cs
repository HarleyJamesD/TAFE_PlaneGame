using System;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    void Start()
    {
        ScoreManager.instance.onScoreChange += UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
