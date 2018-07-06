using Assets.Scripts.Menu.ShopScripts;
using Assets.Scripts.Stats.Characters;
using UnityEngine;

namespace Assets.Scripts.Stats
{
    [CreateAssetMenu(fileName = "New Artifact", menuName = "Preociall Shadow/Artifact")]
    [System.Serializable]
    public class Artifact : ShopItem
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



        public float DamageMultiplierBonus; //Бонус к множителю урона. Так 0.1f = это +10% к урону
        public float HealthMultiplierBonus; //Бонус к множителю здоровья  
        public float DamageAddBonus; //Плюс урона этого артефакта. Прибавляется напрямую к
        public float HealthAddBonus; //Плюс здоровья этого артефакта

        //TODO надо как-то сделать этот метод нормальным
        public virtual CharacterBase AddBonuses(CharacterBase stats)
        {
            //TODO 
            /*
            stats.DamageMultiplier += DamageMultiplierBonus;
            stats.HealthMultiplier += HealthMultiplierBonus;
            stats.DamageAdd += DamageAddBonus;
            stats.BaseHealth += HealthAddBonus;
            */
            return stats;
        }

        public virtual void UseInGameEffect()
        {

        }

        public string GetEffectsString()
        {
            string effects = "";
            if (DamageAddBonus != 0)
            {
                effects += "+" + DamageAddBonus + " к урону";
            }
            if (DamageMultiplierBonus != 0)
            {
                effects +=
                    effects == "" ? "" : ", ";
                effects += "+" + 100 * DamageMultiplierBonus + "%" + " к урону";
            }
            if (HealthAddBonus != 0)
            {
                effects +=
                    effects == "" ? "" : ", ";
                effects += "+" + HealthAddBonus + " к здоровью";
            }
            if (HealthMultiplierBonus != 0)
            {
                effects +=
                    effects == "" ? "" : ", ";
                effects += "+" + 100 * HealthMultiplierBonus + "%" + " к здоровью";
            }

            if (effects != "") effects += ".";

            return effects;
        }

    }
}
