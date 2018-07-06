using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Menu.ShopScripts;
using Assets.Scripts.Stats.Characters;
using UnityEngine;

[CreateAssetMenu(fileName = "Preociall Shadow/SpriteStyle")]
public static class SpriteStyle
{
    public static string IconsFolderPath = "Icons/";
    public static string ElementIconFolderPath = IconsFolderPath + "Elements/";
    public static string CurrencyIconFolderPath = IconsFolderPath + "Currency/";

    public static Sprite GetElementIcon(ElementName element)
    {
        return Resources.Load<Sprite>(ElementIconFolderPath + element);
    }
    public static Sprite GetCurrencyIcon(CurrencyName currency)
    {
        return Resources.Load<Sprite>(CurrencyIconFolderPath + currency);
    }


}
