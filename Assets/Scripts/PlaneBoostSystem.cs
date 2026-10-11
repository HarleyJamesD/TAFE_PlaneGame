using System;
using UnityEngine;

public class PlaneBoostSystem : MonoBehaviour
{
    [SerializeField] private UIManager playerUI;
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
    void Start()
    {
        if (playerUI==null)
        {
            throw new Exception("Player UI is null for Plane Boost System.");
        }
        GameManager.instance.onBalloonPop += AddBoost;
        BoostCount = startingBoostAmount;
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
