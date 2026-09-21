using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public InputSystem_Actions inputActions;
    private InputAction pauseAction;

    private void Awake()
    {
        //Instantiate input action map
        inputActions = new InputSystem_Actions();

        //Destroy this instance if not the first instance
        if (instance!=null && instance!=this) {
            Destroy(this);
            return;
        }
        instance = this;

        pauseAction = inputActions.UI.Pause;
        pauseAction.performed += Pause;
    }

    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private bool isGamePaused = false;
    public Action<bool> onGamePause;
    private void Pause(InputAction.CallbackContext context)
    {
        if (isGamePaused)
        {
            //resume game
            Time.timeScale = 1f;
            isGamePaused = false;
        }
        else {
            //Pause game
            Time.timeScale = 0f;
            isGamePaused = true;
        }
        onGamePause.Invoke(isGamePaused);
    }

    public Action onPlayerDeath; //Called in camera controller
    public void PlayerDead()
    {
        onPlayerDeath.Invoke();
    }
}
