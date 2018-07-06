using Assets.Scripts.Menu.ShopScripts;
using UnityEngine;

namespace Assets.Scripts.Stats
{
    [CreateAssetMenu(fileName = "New Magic", menuName = "Preociall Shadow/Magic")]
    public class Magic : ShopItem
    {
        //Базовые
        /*
        //От ShopItem наследуются следующие поля
        public string Name;
        public Sprite Portrait; //Портрет в магазине
        public Race Race; //раса
        public string Description; //Описание
        public int Cost;
        public int RequiredLevel;
        public CurrencyName CurrencyName;
        public bool IsBuyable;
        public bool IsOwned;
        */

        public float Cooldown; //Длительность передарядки
        public float Mana;  //Используемая мана
        public float Damage;  //Средний урон


        public virtual void UsePassiveEffect()
        {
            Debug.Log(Name + " magic is in the game!");
        }
        public virtual void UseActiveEffect()
        {
            Debug.Log(Name + " magic was used!");
        }


    }
}
