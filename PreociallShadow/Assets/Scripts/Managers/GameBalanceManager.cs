
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public static class GameBalanceManager
    {
        public static float PlayerExpirienceBonus = 1f; //Множитель получаемого опыта
        public static float PlayerCashBonus = 1f; //Множитель получаемой валюты

        public static float PlayerDamageBonus = 1f;
        public static float PlayerDefenceBonus = 1f;

        public static float EnemyDamageBonus = 1f;
        public static float EnemyDefenceBonus = 1f;

        public static float HpMultPerLevel = 0.1f; //Прирост здоровья за уровень (множитель)
        public static float DamageMultPerLevel = 0.1f; //Прирост урона за уровень (множитель)

        public static int MaxLevel = 50; //Максимальный уровань в игре
        public static float LevelExpDelta = 0.2f; //насколько больше (в процентах) опыта нужно на каждом уровне.
        //опыт_для_следующего_уровня = опыт_для_этого_уровня + опыт_для_этого_уровня * LevelDelta; 
        public static int ExpToFirstLevel = 100;

        //Распределание требуемого опыта по уровням.
        public static int GetExpForLevel(int level)
        {
            if (level > MaxLevel) return -1;
            if (level == 0) return 0;

            int a = 0;
            int b = ExpToFirstLevel;
            int c = 0;

            //Принцип Фибоначчи, с добавленным множителем. Так мы получаем распределение со значениями 0, 100, 120, 144 итд
            for (int i = 0; i < level; i++)
            {
                c = Mathf.RoundToInt(a * LevelExpDelta + b);
                a = b;
                b = c;
            }
            return c;
        }

        public static int GetExpForMaxLevel()
        {
            return GetExpForLevel(MaxLevel);
        }

        //Обратная функция - какой уровень соответствует введёному количесву опыта
        public static int GetLevelOnExp(int exp)
        {
            if (exp <= GetExpForLevel(1))
                return 0;

            int neededExp = 0;
            int i = 0;
            while (neededExp < exp)
            {
                neededExp = GetExpForLevel(i);
                i++;
            }
            return i;
        }

        //Возвращает прогресс на текущем уровне в диапазоне [0.0, 1.0] 
        public static float GetLevelProgress(int exp)
        {
            int currentLvl = GetLevelOnExp(exp);

            if (currentLvl == MaxLevel)
            {
                return 1f;
            }

            int expNeeded = GetExpForLevel(currentLvl + 1); //Сколько всего опыта нужно до следующего

            int expToThisLevel = GetExpForLevel(currentLvl); //Сколько опыта нужно для этого уровня
            int expDelta = expNeeded - expToThisLevel;  //Сколько опыта между следующим и нашим уровнем
            int expRemaining = expNeeded - exp; //Сколько опыта осталось для след уровня
            int progressOnCurrLevel = expDelta - expRemaining;


            return (float)progressOnCurrLevel / (float)expDelta;
        }
        
    }
}
