using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts
{
    public class CurrencyDisplay : MonoBehaviour
    {

        /// <summary>
        /// Скрывать всё кроме только рубинов и кристаллов
        /// </summary>
        public bool ShowAll = true; 

        public Text RubyText;
        public Text PreoCryText;
        public Text GoldText;
        public Text SoulsText;
        public Text DarkCryText;
        public Text LightCryText;

        public Text LevelText;
        public Slider ExpirienceSlider;

        private GlobalProgressManager _global;

        private void Start()
        {
            _global = GlobalProgressManager.Instance;
            UpdateCurrency();
        }

        public void UpdateCurrency()
        {
            UpdateRubyAndCrystall();

            if (ShowAll)
            {
                UpdateOthers();
            }

            UpdateExp();
        }

        private void UpdateExp()
        {
            LevelText.text = _global.GetCurrencyValue(CurrencyName.Level).ToString();
            ExpirienceSlider.value = 1f; //TODO _global.GetLevelProgress();
        }

        private void UpdateOthers()
        {
            GoldText.text = _global.GetCurrencyValue(CurrencyName.Gold).ToString();
            SoulsText.text = _global.GetCurrencyValue(CurrencyName.Souls).ToString();
            DarkCryText.text = _global.GetCurrencyValue(CurrencyName.DarkCrystal).ToString();
            LightCryText.text = _global.GetCurrencyValue(CurrencyName.LightCrystal).ToString();
        }

        private void UpdateRubyAndCrystall()
        {
            RubyText.text = _global.GetCurrencyValue(CurrencyName.Ruby).ToString();
            PreoCryText.text = _global.GetCurrencyValue(CurrencyName.PreociallCrystal).ToString();
        }
    }
}
