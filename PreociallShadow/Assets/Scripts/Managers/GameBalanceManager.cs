
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameBalanceManager : MonoBehaviour
    {
        public float PlayerExpirienceBonus = 1f; //Множитель получаемого опыта
        public float PlayerCashBonus = 1f; //Множитель получаемой валюты

        public float PlayerDamageBonus = 1f;
        public float PlayerDefenceBonus = 1f;

        public float EnemyDamageBonus = 1f;
        public float EnemyDefenceBonus = 1f;


        public float HpMultPerLevel = 0.1f; //Прирост здоровья за уровень (множитель)
        public float DamageMultPerLevel = 0.1f; //Прирост урона за уровень (множитель)

    }
}
