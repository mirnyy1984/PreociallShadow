using System;
using UnityEngine;

namespace Assets.Scripts.Menu.ShopScripts
{
    //Это наследуют все разновидности слотов в магазине
    internal abstract class ShopSlot : MonoBehaviour
    {
        public Shop Shop;

        public abstract void SetItemToDisplay(Component item);
        public abstract void DrawAvailableSlot();
        public virtual void DrawTooExpensiveSlot() { }
        public virtual void DrawTooHighLevelSlot() { }
        public virtual void DrawBoughtSlot() { }
        public virtual void DrawInventorySlot() { }

        public abstract void BuyThis();
    }
}
