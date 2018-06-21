using Assets.Scripts.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts.ShopPages.Slots.Items
{
    class Slot : MonoBehaviour, ISlot
    {
        #region Public Ui references

        public Image Poritait;
        public Text NameTxt;
        public Text RequiredLevelTxt;
        public Text PriceTxt;
        public Text EffectsTxt;
        public Text DescriptionTxt;
        public Text RaceTxt;

        #endregion

        public void DrawAwailable(ShopItem item)
        {
            var artifact = item as Artifact;

            if (artifact == null)
            {
                print("ошибка");
                return;
            }

            DrawBase(artifact);
        }
        public void DrawBought(ShopItem item)
        {
            var artifact = item as Artifact;

            if (artifact == null)
            {
                print("ошибка");
                return;
            }

            DrawBase(artifact);
            PriceTxt.text = "Куплено";
        }
        public void DrawInventory(ShopItem item)
        {
            var artifact = item as Artifact;

            if (artifact == null)
            {
                print("ошибка");
                return;
            }

            DrawBase(artifact);
            PriceTxt.text = "";
        }
        public void DrawTooExpensive(ShopItem item)
        {
            var artifact = item as Artifact;

            if (artifact == null)
            {
                print("ошибка");
                return;
            }

            DrawBase(artifact);
        }
        public void DrawTooHighLevel(ShopItem item)
        {
            var artifact = item as Artifact;

            if (artifact == null)
            {
                print("ошибка");
                return;
            }

            DrawBase(artifact);
            DescriptionTxt.text = "Вам необходим уровень " + artifact.RequiredLevel;
            PriceTxt.text = "???";
        }

        private void DrawBase(Artifact artifact)
        {
            NameTxt.text = artifact.Name;
            Poritait.sprite = artifact.Portrait;
            RequiredLevelTxt.text = artifact.RequiredLevel + " уровень";

            if (EffectsTxt != null)
            {
                string effects = artifact.GetEffectsString();
                EffectsTxt.text = effects;
            }

            PriceTxt.text = "Цена: " + artifact.Cost + " " + artifact.CurrencyName; //TODO перевод валют на русский
            DescriptionTxt.text = artifact.Description;
            RaceTxt.text = artifact.Race.ToString(); //TODO перевод расы на русский
        }
    }
}
