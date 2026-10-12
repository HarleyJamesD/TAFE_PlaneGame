using System;
using UnityEngine;

public class PlaneBoostSystem : MonoBehaviour
{
    [SerializeField] private int startingBoostAmount = 3;
    public int BoostCount { 
        get { return boostCount; }
        set { 
            boostCount = value; 
            OnBoostCountChanged?.Invoke(boostCount);
        }
    }
    private int boostCount;
    public event Action<int> OnBoostCountChanged;

    private void Awake()
    {
        BoostCount = startingBoostAmount;
    }

    void OnEnable()
    {
        GameManager.instance.onBalloonPop += AddBoost;
    }

    private void OnDisable()
    {
        GameManager.instance.onBalloonPop -= AddBoost;
    }

    public void AddBoost()
    {
        BoostCount++;
    }

    public bool TryUseBoost()
    {
        if (boostCount <= 0) return false;

        BoostCount--;
        return true;
    }
}
