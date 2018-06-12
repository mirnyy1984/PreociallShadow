using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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

}
