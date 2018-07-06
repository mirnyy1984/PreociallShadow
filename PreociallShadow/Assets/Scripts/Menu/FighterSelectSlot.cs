using Assets.Scripts.Stats;
using Assets.Scripts.Stats.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    internal class FighterSelectSlot : MonoBehaviour
    {
        public FighterSelectScreen SelectScreen;

        [SerializeField]
        private CharacterBase _stats;

        private Image _portrait;
        //private string _name;

        private void Awake()
        {
            _portrait = GetComponent<Image>();
        }

        public void SetCharacterStats(CharacterBase stats)
        {
            _stats = stats;
            _portrait.sprite = stats.Portrait;
        }

        //Вызывается с Button'a
        public void OnClick()
        {
            SelectScreen.SelectFighter(_stats);
        }

    }
}
