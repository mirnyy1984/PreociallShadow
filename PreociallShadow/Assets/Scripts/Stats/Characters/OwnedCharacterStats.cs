using System;
using System.Collections.Generic;
using Assets.Scripts.Player;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Assets.Scripts.Stats.Characters
{
    //То, насколько мы прокачали персонажа
    [Serializable]
    public class OwnedCharacterStats : MonoBehaviour
    {
        public CharacterBase BaseCharacter; //База персонажа, к которому относится этот класс

        //Прокачиваемые
        public float DamageMultiplier = 1f;  //Множитель урона (базового урона от приёмов) 
        public float DamagePlus = 0f;  //Прямой бонус к урону
        public float HealthMultiplier = 1f;  //Множитель здоровья 
        public float HealthPlus = 0f;  //Прямой бонус к здоровью
        public float AttackSpeed = 1f;  //Скорость атаки

        public List<PlayerBehavoir> LearnedPunchesBehavoirs; //Список изученых приёмов
        public Magic EquippedMagic; //Текущая магия 
        public List<Artifact> EquippedArtifacts; //Текущийе артефакты //TODO можно ли надевать несколько артефактов
        public int ArtifactCount = 1;
        public int EnergyPlus = 0; //Энергия - тратится 1 за бой, восстанавливается за время или донат.
        public float ExpirienceMultiplier = 1f; //Множитель опыта для этого героя
        public float CashMultiplier = 1f; //Множитель денег для этого героя
        public int CurrentEnergy = 0; //Энергия сейчас.

        //Статус
        public int Level = 0; //Текущий уровень
        public int SkillPoints; //Валюта очки навыков. Один очок даётся за один уровень. Тратятся на приёмы.
        public float Expirience; //Текущий опыт
        public int Energy; //Энергия - тратится 1 за бой, восстанавливается за время или донат.

        public int GetMaxEnergy()
        {
            return BaseCharacter.MaxEnergy + EnergyPlus;
        }

        //TODO countStatsWithArtifacts

        //TODO AddExp(int amount)

    }
}
