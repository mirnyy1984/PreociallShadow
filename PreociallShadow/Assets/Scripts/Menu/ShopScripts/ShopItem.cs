using Assets.Scripts.Stats;
using UnityEngine;

namespace Assets.Scripts.Menu.ShopScripts
{
    public abstract class ShopItem : ScriptableObject
    {
        public string Name;
        public Sprite Portrait; //Портрет в магазине
        public Race Race; //раса
        public string Description; //Описание
        public int Cost; //Цена в магазине
        public int RequiredLevel; //Нужный уровень в магазине
        public CurrencyName CurrencyName; //Валюта в магазине
        public bool IsBuyable = true; //продаётся ли в магазине
        public bool IsOwned = false; //Купил (получил) ли игрок этот предмет 
    } 
}
