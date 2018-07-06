using System;
using Assets.Scripts.Managers;
using Assets.Scripts.Menu.ShopScripts;
using UnityEditor;
using UnityEngine;


public class Cheats : MonoBehaviour
{

    private void OnGUI()
    {
        if (GUILayout.Button("Add 1000 money"))
        {
            Add1000OfAllMoney();
        }
    }
    public void Add1000OfAllMoney()
    {
        Array currencies = Enum.GetValues(typeof(CurrencyName));

        foreach (CurrencyName c in currencies)
        {
            GlobalProgressManager.Instance.AddCurrency(c, 1000);
        }
    }

}
