using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        //Destroy this instance if not the first instance
        if (instance!=null && instance!=this) {
            Destroy(this);
            return;
        }
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Action onPlayerDeath; //Called in camera controller
    public void PlayerDead()
    {
        onPlayerDeath.Invoke();
    }
}
