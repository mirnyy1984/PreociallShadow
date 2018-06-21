using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts
{
    public class CurrencyBar : MonoBehaviour
    {

        public Text RubyText;
        public Text PreoCryText;
        public Text GoldText;
        public Text SoulsText;
        public Text DarkCryText;
        public Text LightCryText;

        public Text LevelText;
        public Slider ExpirienceSlider;

        private void Start()
        {
            UpdateCurrency();
        }

        public void UpdateCurrency()
        {
            RubyText.text = GlobalProgressManager.Instance.GetCurrencyValue(CurrencyName.Ruby).ToString();
            PreoCryText.text = GlobalProgressManager.Instance.GetCurrencyValue(CurrencyName.PreociallCrystal).ToString();
            GoldText.text = GlobalProgressManager.Instance.GetCurrencyValue(CurrencyName.Gold).ToString();
            SoulsText.text = GlobalProgressManager.Instance.GetCurrencyValue(CurrencyName.Souls).ToString();
            DarkCryText.text = GlobalProgressManager.Instance.GetCurrencyValue(CurrencyName.DarkCrystal).ToString();
            LightCryText.text = GlobalProgressManager.Instance.GetCurrencyValue(CurrencyName.LightCrystal).ToString();

            LevelText.text = GlobalProgressManager.Instance.GetCurrencyValue(CurrencyName.Level).ToString();
            ExpirienceSlider.value = GlobalProgressManager.Instance.GetLevelProgress();
        }

    }
}
