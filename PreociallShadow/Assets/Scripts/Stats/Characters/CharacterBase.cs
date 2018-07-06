using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using Assets.Scripts.Managers;
using Assets.Scripts.Menu.ShopScripts;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Stats.Characters
{
//Базовые характеристики персонажа
    [CreateAssetMenu(fileName = "New Character", menuName = "Preociall Shadow/Character")]
    [System.Serializable]
    public class CharacterBase : ShopItem
    {
        //от shopitem:
        /*
        public string Name;
        public Sprite Portrait; //Портрет в магазине
        public Race Race; //раса
        public string Description; //Описание
        public int Cost; //Цена в магазине
        public int RequiredLevel; //Нужный уровень в магазине
        public CurrencyName CurrencyName; //Валюта в магазине
        public bool IsBuyable = true; //продаётся ли в магазине
        public bool IsOwned = false; //Купил (получил) ли игрок этот предмет 
        */

        //Базовые

        public string SkinName = "Default Skin"; //Скин (костюм)

        public float Health = 1000f; //Базовое здоровье
        public float Damage = 1f; //Множитель урона для всех приёмов
        public float AttackSpeed = 1f; //Скорость атаки
        public int ArtifactCapacity = 1; //Сколько артефактов можно взять этому герою
        public int MaxEnergy = 3; //Энергия - тратится 1 за бой, восстанавливается за время или донат.
        public List<PlayerBehavoir> PunchesBehavoirs; //Список базовых приёмов
        public int Level = 0; //Начальный уровень //TODO Нужна ли эта фича? Босс/герой которого мы победили появляется с определённым уровнем в магазине.
        public ElementName Element; //Стихия
        public GameObject CharacterPrefab; //Объект с мешем и анимацией
        
        [SerializeField]
        public OwnedCharacterStats OwnedStats = new OwnedCharacterStats();

        public int GetMaxEnergy()
        {
            if (OwnedStats == null)
            {
                return MaxEnergy;
            }
            return OwnedStats.GetMaxEnergy();
        }
        
        public int GetUnusedEnergy()
        {
            if (OwnedStats == null)
            {
                return MaxEnergy;
            }
            return OwnedStats.Energy;
        }

        //TODO для всех стихий (когда станет известно сколько их)
        //Бонкус к урону по стихиям
        public float DamageToPhysical = 0f;

        //TODO для всех стихий (когда станет известно сколько их)
        //Защита от урона стихий
        public float DefensePhysical = 0f; //Защита

        //Получить статы с учётом артефактов и уровней. Только этим методом получаем статы в геймМенеджер и инвентарь
        public static CharacterBase CountCharacterToLevel(CharacterBase character, int level)
        {
            for (int i = 0; i < level; i++) { 
                character.Health *= GameBalanceManager.HpMultPerLevel;
                character.Damage *= GameBalanceManager.DamageMultPerLevel; //Множитель урона для всех приёмов
            }
            return character;
        }

        public static CharacterBase GetRandomEnemyCharacter(int level)
        {
            return null;
        }
    }
}
