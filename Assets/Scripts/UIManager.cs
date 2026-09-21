using System;
using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private float pauseMenuShowDelay = 0.7f;
    private void Awake()
    {
        GameManager.instance.onGamePause += PauseGame;
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
}
