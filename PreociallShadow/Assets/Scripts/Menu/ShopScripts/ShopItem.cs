using UnityEngine;

namespace Assets.Scripts.Menu.ShopScripts
{
    
    public abstract class ShopItem : MonoBehaviour
    {
        public string Name;
        public int Cost; //Цена в магазине
        public int RequiredLevel; //Нужный уровень в магазине
        public CurrencyName CurrencyName; //Валюта в магазине
        public bool IsBuyable = true; //продаётся ли в магазине
        public bool IsOwned = false; //Купил (получил) ли игрок этот предмет 
    } 
}
