using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Artifact : MonoBehaviour
{
    //Базовые
    [SerializeField]
    public string Name = "Artifact";
    public Sprite Portrait; //Портрет в магазине
    public bool IsBuyable = true;  //продаётся ли в магазине
    public float ShopCost = 0f; //Цена в магазине
    public CurrencyName Currency; //Валюта
    public bool Owned;  //Купил (получил) ли игрок этот артефакт 
    public Race Race; //раса
    public int Level;
    public string Description; //Описание

    public float DamageMultiplierBonus; //Бонус к множителю урона. Так 0.1f = это +10% к урону
    public float HealthMultiplierBonus; //Бонус к множителю здоровья  
    public float DamageAddBonus; //Плюс урона этого артефакта. Прибавляется напрямую к
    public float HealthAddBonus; //Плюс здоровья этого артефакта

    public virtual CharacterStats AddBonuses(CharacterStats stats)
    {
        stats.DamageMultiplier += DamageMultiplierBonus;
        stats.HealthMultiplier += HealthMultiplierBonus;
        stats.DamageAdd += DamageAddBonus;
        stats.Health += HealthAddBonus;
        print(transform.name + " art added bonuses!");
        return stats;
    }

    public virtual void GetInGameEffect()
    {
        print(transform.name + " art is in game!");
    }

}
