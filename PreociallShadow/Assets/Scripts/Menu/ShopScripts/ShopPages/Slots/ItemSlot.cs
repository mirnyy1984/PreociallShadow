using Assets.Scripts.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts.ShopPages.Slots
{
    class ItemSlot : MonoBehaviour
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
            if (item is Artifact)
            {
                DrawBase(item as Artifact);
            }
        }

        private void DrawBase(Artifact artifact)
        {
            NameTxt.text = artifact.Name;
            Poritait.sprite = artifact.Portrait;

            if (artifact.RequiredLevel > 0)
                RequiredLevelTxt.text = artifact.RequiredLevel + " уровень";
            else
                RequiredLevelTxt.text = "";

            if (EffectsTxt != null)
            {
                string effects = artifact.GetEffectsString();
                EffectsTxt.text = effects;
            }
            if (PriceTxt != null)
            {
                PriceTxt.text = "Цена: " + artifact.Cost + " " + artifact.CurrencyName; //TODO перевод валют на русский
            }
            DescriptionTxt.text = artifact.Description;
            RaceTxt.text = artifact.Race.ToString();
        }

        public void DrawBought(ShopItem item)
        {
            if (item is Artifact)
            {
                DrawBase(item as Artifact);
            }
            PriceTxt.text = "Куплено";
        }

        public void DrawInInventory(ShopItem item)
        {
            if (item is Artifact)
            {
                DrawBase(item as Artifact);
            }
            PriceTxt.text = "";
        }

        public void DrawTooExpensive(ShopItem item)
        {
            if (item is Artifact)
            {
                DrawBase(item as Artifact);
            }
        }

        public void DrawTooHighLevel(ShopItem item)
        {
            if (item is Artifact)
            {
                DrawBase(item as Artifact);
                NameTxt.text = "";
                DescriptionTxt.text = "Необходим уровень " + item.RequiredLevel;
            }
        }

        public void OnSlotClick()
        {
            transform.parent.GetComponent<SlotParent>().Buy();
        }
    }
}
