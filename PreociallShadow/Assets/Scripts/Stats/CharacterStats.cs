using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEditorInternal;
using UnityEngine;

namespace Assets.Scripts.Stats
{
//Характеристики персонажа

    public interface ICharacterStats
    {
        CharacterStats GetStats();
    }

    [System.Serializable]
    public class CharacterStats : MonoBehaviour, ICharacterStats
    {
        //Базовые
        public string Name = "Character";
        public string SkinName = "Default Skin"; //Скин (костюм)
        public Sprite Portrait; //Портрет в магазине
        public float Health = 1000f; //Базовое здоровье
        public GameObject CharacterGameObject; //Объект с мешем и анимацией
        public Race Race; //Собственно, раса

        //Прокачиваемые
        public float DamageMultiplier = 1f;  //Множитель урона (базового урона от приёмов) 
        public float DamageAdd = 0f;  //Прямой бонус к урону
        public float HealthMultiplier = 1f;  //Множитель здоровья 
        public float AttackSpeed = 1f;  //Скорость атаки
        public float Defense = 0f; //Защита //TODO пока защита никак не работает в игре
        public List<PlayerBehavoir> PunchesBehavoirs; //Список изученых приёмов
        public int ArtifactCapacity = 1; //Сколько артефактов можно взять этому герою
        public List<Artifact> EquippedArtifacts; //Текущийе артефакты
        //TODO//public Magic EquippedMagic; //Текущая магия 
        public int MaxEnergy = 5; //Энергия - тратится 1 за бой, восстанавливается за время или донат.
        public float ExpirienceMultiplier = 1f; //Множитель опыта для этого героя
        public float CashMultiplier = 1f; //Множитель денег для этого героя
    
        //Статус
        public int Level = 0; //Текущий уровень
        public int SkillPoints; //Валюта очки навыков. Один очк даётся за один уровень. Тратятся на приёмы.
        public float Expirience; //Текущий опыт
        public int Energy; //Энергия - тратится 1 за бой, восстанавливается за время или донат.

        //Получить статы с учётом артефактов и уровней. Только этим методом получаем статы в геймМенеджер и инвентарь
        public CharacterStats GetStats()
        {
            CharacterStats newStats = this;

            if (Level > 0)
            {
                newStats.Health *= HealthMultiplier;
            }

            if (EquippedArtifacts.Count > 0)
            {

                foreach (var artifact in  EquippedArtifacts)
                {
                
                }
            }

            return newStats;
        }

    }
}