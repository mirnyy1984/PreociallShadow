using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GlobalProgressManager : MonoBehaviour
{
    public const int MaxLevel = 50; //Максимальный уровань в игре
    public const float LevelDelta = 1.1f; //насколько больше опыта нужно на каждом уровне. expToNextLvl = expToThisLvl * LevelDelta; 
    public const int ExpToFirstLevel = 100; 

    //Деньги 
    readonly Dictionary<CurrencyName, int> _currencyValues = new Dictionary<CurrencyName, int>();

    private GlobalProgressManager Instance;

    #region Singleton
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(this);
        }
    }

    #endregion

    void Start()
    {
        //if (firstEnterToGame)
        {
            _currencyValues[CurrencyName.Level] = 1;
        }
    }


    public bool Spend(CurrencyName currencyName, int amount)
    {
        if (currencyName == CurrencyName.Expirience
            || currencyName == CurrencyName.Level
            || currencyName == CurrencyName.SkillPoints)
        {
            return false;
        }

        int value = _currencyValues[currencyName];
        if (value - amount < 0)
        {
            //TODO сообщение - нудостаточно денег
            return false;
        }
        else
        {
            value -= amount;
            _currencyValues[currencyName] = value;
            return true;
        }
    }

    public void Add(CurrencyName currencyName, int amount)
    {
        if (currencyName == CurrencyName.Expirience)
        {
            AddExp(amount);
            return;
        }
        Spend(currencyName, -amount);
    }

    public void AddExp(int amount)
    {
        _currencyValues[CurrencyName.Expirience] += amount;
        int exp = _currencyValues[CurrencyName.Expirience];

        int newLevel = 0;
        int expToThisLevel = ExpToFirstLevel;
        while (exp >= 0)
        {
            exp -= expToThisLevel;
            newLevel++;
            expToThisLevel = Mathf.RoundToInt(expToThisLevel * LevelDelta);
        }

        if (newLevel != _currencyValues[CurrencyName.Level])
        {
            //TODO you've leveled up message
            _currencyValues[CurrencyName.Level] = newLevel;
        }
    }
  
}