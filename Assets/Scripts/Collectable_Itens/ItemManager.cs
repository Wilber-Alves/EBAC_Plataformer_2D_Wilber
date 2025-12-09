using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public int totalCoins = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        Reset();
    }
    private void Reset()
    {
        totalCoins = 0;


    }
    public void AddCoins(int amount = 1)
    {
        totalCoins += amount;
    }
}
