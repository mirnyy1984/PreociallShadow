using System;
using Assets.Scripts.Menu.ShopScripts;
using Assets.Scripts.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts
{
    internal class ArtifactShopSlot : ShopSlot
    {

#region Public Ui references

        public Image Poritait;
        public Text RequiredLevel;
        public Text PriceTxt;
        public Text EffectsTxt;
        public Text DescriptionTxt;
        public Text RaceTxt;

#endregion


        [SerializeField]
        private Artifact _artifact;

        //TODO реализация DrawUnavailableSlot() и других

        public override void SetItemToDisplay(Component item)
        {
            _artifact = item as Artifact;
        }

        public override void DrawAvailableSlot()
        {
            Poritait.sprite = _artifact.Portrait;
            RequiredLevel.text = _artifact.RequiredLevel + " уровень";

            string effects = "";
            if (_artifact.DamageAddBonus != 0)
            {
                effects += "+" + _artifact.DamageAddBonus + " к урону";
            }
            if (_artifact.DamageMultiplierBonus != 0)
            {
                effects +=
                    effects == "" ? "" : ", ";
                effects += "+" + 100 * _artifact.DamageMultiplierBonus + "%" + " к урону";
            }
            if (_artifact.HealthAddBonus != 0)
            {
                effects +=
                    effects == "" ? "" : ", ";
                effects += "+" + _artifact.HealthAddBonus + " к здоровью";
            }
            if (_artifact.HealthMultiplierBonus != 0)
            {
                effects +=
                    effects == "" ? "" : ", ";
                effects += "+" + 100 * _artifact.HealthMultiplierBonus + "%" + " к здоровью";
            }

            if (effects != "") effects += ".";

            EffectsTxt.text = effects;
            PriceTxt.text = "Цена: " + _artifact.ShopCost + " " + _artifact.Currency; //TODO перевод валют на русский
            DescriptionTxt.text = _artifact.Description;
            RaceTxt.text = _artifact.Race.ToString(); //TODO перевод расы на русский
        }

        public override void BuyThis()
        {
            Shop.BuyArtifact(_artifact);
        }
    }
}