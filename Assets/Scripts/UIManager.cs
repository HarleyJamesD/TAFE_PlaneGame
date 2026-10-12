using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private float pauseMenuShowDelay = 0.7f;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Slider fuelSlider;
    [SerializeField] private RectTransform lifeIconList;

    [Header("Boost UI")]
    [SerializeField] private GameObject boostKnob;
    [SerializeField] private RectTransform boostList;
    [SerializeField] private PlaneBoostSystem boostSystem;

    private void OnEnable()
    {
        GameManager.instance.onGamePause += PauseGame;
        if (ScoreManager.instance!=null)
        {
            ScoreManager.instance.onScoreChange += UpdateScore;
        }

        FuelCountdown.instance.onFuelUpdate += UpdateFuel;
        HealthCount.instance.onRemoveLife += RemoveLife;
        
        if (boostSystem != null)
        {
            boostSystem.OnBoostCountChanged += UpdateBoost;
            //UpdateBoost(boostSystem.BoostCount); //I dont think I need it because OnBoostChanged gets called itself
        }
    }

    private void RemoveLife()
    {
        Destroy(lifeIconList.GetChild(0).gameObject);
    }

    private void UpdateFuel(float percentFill)
    {
        fuelSlider.value = percentFill;
    }

    private void UpdateScore(int totalScore)
    {
        scoreText.text = totalScore.ToString();
    }

    private void OnDisable()
    {
        GameManager.instance.onGamePause -= PauseGame;
        if (boostSystem != null)
        {
            boostSystem.OnBoostCountChanged -= UpdateBoost;
        }
    }

    Coroutine pauseCoroutine;
    private void PauseGame(bool isGamePaused)
    {
        if (isGamePaused)
        {
            pauseCoroutine = StartCoroutine(PauseDelay());
        }
        else
        {
            if (pauseCoroutine != null)
            {
                StopCoroutine(pauseCoroutine);
            }
            pauseMenu.SetActive(false);
        }

        IEnumerator PauseDelay()
        {
            yield return new WaitForSecondsRealtime(pauseMenuShowDelay);
            pauseMenu.SetActive(true);
        }
    }

    private void UpdateBoost(int boostCount)
    {
        // Remove existing icons.
        foreach (Transform child in boostList)
        {
            Destroy(child.gameObject);
        }

        // Create the correct number of icons.
        for (int i = 0; i < boostCount; i++)
        {
            Instantiate(boostKnob, boostList);
        }
    }

    private void FuelPercent(float percent)
    {

    }
}
