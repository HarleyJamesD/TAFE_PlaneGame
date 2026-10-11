using System;
using System.Collections;
using UnityEngine;

public class FuelCountdown : MonoBehaviour
{
    public static FuelCountdown instance;
    [SerializeField] private float totalFuelAmount = 50f;
    private float fuelAmount;
    private bool currentlyCountingDown = true;
    [SerializeField] private float hoopFuelAmount = 10f;

    public event Action<float> onFuelUpdate;
    public event Action onFuelOut;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameManager.instance.onThroughHoop += (int points) =>
        {
            AddFuel(hoopFuelAmount * points);
        };
        ResetFuel();
    }

    private IEnumerator FuelCountdownUpdate()
    {
        if (fuelAmount <= 0)
        {
            onFuelOut.Invoke();
            yield break;
        }
        while (currentlyCountingDown)
        {
            fuelAmount -= Time.deltaTime;
            onFuelUpdate?.Invoke(fuelAmount / totalFuelAmount);
            yield return null;
        }
    }

    public void AddFuel(float addedFuel)
    {
        fuelAmount = Mathf.Min(totalFuelAmount, fuelAmount+addedFuel);
    }

    public void ResetFuel()
    {
        fuelAmount = totalFuelAmount;
        StartCoroutine(FuelCountdownUpdate());
    }
}
