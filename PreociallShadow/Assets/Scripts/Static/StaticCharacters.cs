using Assets.Scripts.Stats;
using Assets.Scripts.Stats.Characters;

namespace Assets.Scripts.Static
{
    //Это нужно для обмена между экраном выбора персонажей и сценой боя
    internal class StaticCharacters
    {
        public static CharacterBase Player;
        public static CharacterBase Enemy;
    }
}
