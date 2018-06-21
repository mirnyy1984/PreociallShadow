using System;
using Assets.Scripts.Managers;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts.ShopPages.Slots.Artifacts
{
    //Слот для магии или артефакта
    internal class SlotParent : MonoBehaviour
    {
        public GameObject AvailableSlotPrefab;
        public GameObject TooExpensiveSlotPrefab;
        public GameObject TooHighLevelSlotPrefab;
        public GameObject BoughtSlotPrefab;
        public GameObject InventorySlotPrefab;

        public Shop Shop;

        [SerializeField] private ShopItem _item;

        public void DrawSlot(ShopItem item)
        {
            this._item = item;

            int playerMoney = GlobalProgressManager.Instance.GetCurrencyValue(item.CurrencyName);
            int playerLevel = GlobalProgressManager.Instance.GetCurrencyValue(CurrencyName.Level);

            GameObject slotGO;
            ItemSlot itemSlotScript;
            //Если уже купили
            if (item.IsOwned)
            {
                slotGO = Instantiate(BoughtSlotPrefab, transform);
                itemSlotScript = slotGO.GetComponent<ItemSlot>();
                itemSlotScript.DrawBought(item);
            }
            //Если не хвататет денег
            else if (item.Cost > playerMoney)
            {
                slotGO = Instantiate(TooExpensiveSlotPrefab, transform);
                itemSlotScript = slotGO.GetComponent<ItemSlot>();
                itemSlotScript.DrawTooExpensive(item);
            }
            //Если не хватет уровня
            else if (item.RequiredLevel > playerLevel)
            {
                slotGO = Instantiate(TooHighLevelSlotPrefab, transform);
                itemSlotScript = slotGO.GetComponent<ItemSlot>();
                itemSlotScript.DrawTooHighLevel(item);
            }
            //Если всё в порядке и можно купить
            else
            {
                slotGO = Instantiate(AvailableSlotPrefab, transform);
                itemSlotScript = slotGO.GetComponent<ItemSlot>();
                itemSlotScript.DrawAwailable(item);
            }

        }

        //Метод из интерфейса. 
        public void SetShop(Shop shop)
        {
            //Использую this явно только для читаемости - присваевается значение переменной этого экземпляра
            this.Shop = shop;
        }

        public void Buy()
        {
            //Shop = GameObject.Find("Shop").GetComponent<Shop>();
            if (!_item.IsOwned)
                Shop.Buy(_item);
            else
            {
                //TODO предложить выбрать героя для экипирования его когда будут готовы герои в инвентаре
            }
        }
    }
}