using System;
using UnityEngine;

public class HealthCount : MonoBehaviour
{
    private int startingLivesCount = 3;
    private int livesCount;
    public static HealthCount instance;
    public event Action onRemoveLife;
    private void Awake()
    {
        instance = this;
        livesCount = startingLivesCount;
    }

    public void RemoveLife()
    {
        onRemoveLife?.Invoke();
        livesCount--;
        //Temporarily call reset fuel here, rather than respawn
        FuelCountdown.instance.ResetFuel();
    }
}
