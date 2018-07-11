using Assets.Scripts.Stats.Characters;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Menu.ShopScripts.ShopPages.Slots
{
    class CharacterSlot : Slot
    {
        public Text SkinTxt;
        public Text PowerTxt;
        public Text DefenceTxt;

        public Image CurrencyIcon;
        public Image ElementIcon;

        public GameObject ReadyEnergySprite;
        public GameObject UsedEnergySprite;

        public RectTransform EnergyParent;

        public override void DrawSlot(ShopItem item)
        {
            base.DrawSlot(item);
            var character = item as CharacterBase;
            SkinTxt.text = character.SkinName;
            if (character.IsOwned)
            {
                LevelTxt.text = character.OwnedStats.Level.ToString();
            }

            PriceTxt.text = character.Cost.ToString("N0");
            //PowerTxt.text = character.GetPower();
            //DefenceTxt.text = character.GetDefence();
            ElementIcon.sprite = SpriteStyle.GetElementIcon(character.Element);
            CurrencyIcon.sprite = SpriteStyle.GetCurrencyIcon(character.CurrencyName);

            DrawEnergy(character);

        }

        private void DrawEnergy(CharacterBase character)
        {
            foreach (Transform energy in EnergyParent)
            {
                Destroy(energy.gameObject);
            }

            int totalenergy = character.GetMaxEnergy();
            int unusedEnergy = character.GetUnusedEnergy();
            int usedEnergy = totalenergy - unusedEnergy;
            for (int i = 0; i < unusedEnergy; i++)
            {
                Instantiate(ReadyEnergySprite, EnergyParent);
            }
            for (int i = 0; i < usedEnergy; i++)
            {
                Instantiate(UsedEnergySprite, EnergyParent);
            }
        }
    }
}
