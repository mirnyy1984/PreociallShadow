using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Menu.ShopScripts.ShopPages.Slots;
using Assets.Scripts.Menu.ShopScripts.ShopPages.Slots.Artifacts;
using UnityEngine;

namespace Assets.Scripts.Menu.ShopScripts.ShopPages
{
    abstract class ShopPage : MonoBehaviour
    {
        public Shop Shop;
        public Transform ShopContent;
        public bool SortOrder = false;
        public GameObject SlotParentPrefab;

        private void OnEnable()
        {
            DrawPage();
        }

        public void DrawPage()
        {
            foreach (Transform child in ShopContent.transform)
            {
                Destroy(child.gameObject);
            }

            var sortedItems = SortItems();
            foreach (var item in sortedItems)
            {
                var slotGo = Instantiate(SlotParentPrefab, ShopContent);
                var slotScript = slotGo.GetComponent<SlotParent>();
                slotScript.SetShop(Shop);
                slotScript.DrawSlot(item);

            }
        }

        //Вывзывается верхней панелью фильтров в магазине
        public abstract List<ShopItem> SortItems();

        public void ReverseSortOrder()
        {
            SortOrder = !SortOrder;
            DrawPage();
        }
    }
}
