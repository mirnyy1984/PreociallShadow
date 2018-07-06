using Assets.Scripts.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts.ShopPages.Slots
{
    //Слот для магии или артефакта
    internal class Slot : MonoBehaviour
    {
        [SerializeField] private ShopItem _item; //То, что лежит в этом слоте

        #region Public Ui references
        
        public Image Portrait;
        public Text NameTxt;
        public Text LevelTxt;
        public Text PriceTxt;
        public Text EffectsTxt;
        public Text DescriptionTxt;
        public Text RaceTxt;

        #endregion

        

        public virtual void DrawSlot(ShopItem item)
        {
            DrawBase(item);
        }

        private void DrawBase(ShopItem item)
        {
            this._item = item;

            NameTxt.text = item.Name;
            Portrait.sprite = item.Portrait;

            if (item.RequiredLevel > 0)
                LevelTxt.text = item.RequiredLevel + " уровень";
            else
                LevelTxt.text = "";

            //Для артефактов ещё есть эффекты
            if (item is Artifact && EffectsTxt != null)
            {
                string effects = ((Artifact) item).GetEffectsString();
                EffectsTxt.text = effects;
            }

            if (PriceTxt != null)
            {
                PriceTxt.text = "Цена: " + item.Cost + " " + item.CurrencyName; //TODO перевод валют на русский
            }

            if (DescriptionTxt != null)
            {
                DescriptionTxt.text = item.Description;
            }

            RaceTxt.text = item.Race.ToString();
        }

        public void OnSlotClick()
        {
            //TODO not buy characters but go to 
            Buy();
        }

        public void Buy()
        {
            if (!_item.IsOwned)
                Shop.Instance.Buy(_item);
            else
            {
                //TODO предложить выбрать героя для экипирования когда будут готовы герои в инвентаре
            }

        }



    }
}