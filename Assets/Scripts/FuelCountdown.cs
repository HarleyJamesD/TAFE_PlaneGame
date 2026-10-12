using System;
using System.Collections;
using UnityEngine;

public class FuelCountdown : MonoBehaviour
{
    public static FuelCountdown instance;
    [SerializeField] private float totalFuelAmount = 10f;
    private float fuelAmount;
    private bool currentlyCountingDown = true;
    [SerializeField] private float hoopFuelAmount = 10f;

    public event Action<float> onFuelUpdate;
    //public event Action onFuelOut;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
        GameManager.instance.onThroughHoop += (int points) =>
        {//Cant be unsubscribed
            AddFuel(hoopFuelAmount * points);
        };
        ResetFuel();
    }

    private IEnumerator FuelCountdownUpdate()
    {
        while (currentlyCountingDown)
        {
            Debug.Log("Fuel Amount " + fuelAmount + " total fuel " + totalFuelAmount);
            if (fuelAmount <= 0)
            {
                Debug.Log("Tried to remove life");
                HealthCount.instance.RemoveLife();
                yield break;
            }
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
