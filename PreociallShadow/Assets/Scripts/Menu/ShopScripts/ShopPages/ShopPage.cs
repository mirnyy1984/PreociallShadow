using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Menu.ShopScripts.ShopPages.Slots;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts.ShopPages
{
    internal abstract class ShopPage : MonoBehaviour
    {
        public Shop Shop;
        public RectTransform ShopContent;

        public bool SortOrder = false;
        public bool HideTooHighLevel = false;

        public GameObject FilterWindowGO;
        public Animator FilterWindowAnimator;

        public GameObject ShopSlotPrefab;
        public GameObject OwnedSlotPrefab;

        public Dropdown RaceFilterDropdown;
        public Dropdown SortOrderDropdown;
        public Toggle ReverseOrderToggle;
        public Toggle HideOwnedToggle;


        public void EnableContent()
        {
            ShopContent.gameObject.SetActive(true);
            DrawPage();
        }
        public void DisableContent()
        {
            ShopContent.gameObject.SetActive(false);
        }

        public void DrawPage()
        {
            foreach (Transform child in ShopContent.transform)
            {
                Destroy(child.gameObject);
            }
            var sortedItems = SortItems();

            HideNotBuyable(ref sortedItems);
            
            //Развернём список по кнопке справа
            if (ReverseOrderToggle.isOn)
            {
                sortedItems.Reverse();
            }

            foreach (var item in sortedItems)
            {
                if (item.IsOwned && HideOwnedToggle.isOn)
                {
                    continue;
                }

                //Спавним слот продающегося или купленного предмета
                var slotGO = Instantiate(item.IsOwned ? OwnedSlotPrefab : ShopSlotPrefab, ShopContent);

                var itemSlotScript = slotGO.GetComponent<Slot>();
                itemSlotScript.DrawSlot(item);
            }
        }

        private void HideNotBuyable(ref List<ShopItem> items)
        {
            items = items.Where(
                x => x.IsBuyable
            ).ToList();

            if (HideTooHighLevel)
            {
                HideTooHighLevelFromList(ref items);
            }
        }
        private void HideTooHighLevelFromList(ref List<ShopItem> items)
        {
            int playerLevel = GlobalProgressManager.Instance.GetCurrencyValue(CurrencyName.Level);
            items = items.Where(
                x => x.RequiredLevel <= playerLevel
            ).ToList();
        }
        //Вывзывается верхней панелью фильтров в магазине
        //Реализуется в скрипте для каждой страницы (ShopPageArtifacts (or Characters) )
        public abstract List<ShopItem> SortItems();
        
        public void ShowFilterWindow()
        {
            FilterWindowGO.SetActive(true);
            FilterWindowAnimator.SetTrigger("Show");
        }

        public void HideFilterWinwow()
        {
            FilterWindowAnimator.SetTrigger("Hide");
        }
    }
}
