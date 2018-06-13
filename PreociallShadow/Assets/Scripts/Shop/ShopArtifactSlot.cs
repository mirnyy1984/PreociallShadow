using Assets.Scripts.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    [ExecuteInEditMode] //нужно чтобы в эдиторе были видны обновления слотов
    class ShopArtifactSlot : MonoBehaviour
    {
        public Image Poritait;
        public Text RequiredLevel;
        public Text PriceTxt;
        public Text EffectsTxt;
        public Text DescriptionTxt;
        public Text RaceTxt;

        public Artifact Artifact;

        void Start()
        {
            DrawSlot();
        }

        private void DrawSlot()
        {
            //TODO если нам недоступен этот предмет - DrawUnavailableSlot()
            //TODO если мы купили этот предмет - DrawOwnedSlot()
            AvailableSlot();
        }

        private void AvailableSlot()
        {
            Poritait.sprite = Artifact.Portrait;
            RequiredLevel.text = Artifact.Level + " уровень";

            string effects = "";
            if (Artifact.DamageAddBonus != 0)
            {
                effects += "+" + Artifact.DamageAddBonus + " к урону";
            }
            if (Artifact.DamageMultiplierBonus != 0)
            {
                effects +=
                    effects == "" ? "" : ", ";
                effects += "+" + 100 * Artifact.DamageMultiplierBonus + "%" + " к урону";
            }
            if (Artifact.HealthAddBonus != 0)
            {
                effects +=
                    effects == "" ? "" : ", ";
                effects += "+" + Artifact.HealthAddBonus + " к здоровью";
            }
            if (Artifact.HealthMultiplierBonus != 0)
            {
                effects +=
                    effects == "" ? "" : ", ";
                effects += "+" + 100 * Artifact.HealthMultiplierBonus + "%" + " к здоровью";
            }

            if (effects != "") effects += ".";

            EffectsTxt.text = effects;
            //TODO цвет текста цены в зависимости от того хватает нам денег
            PriceTxt.text = "Цена: " + Artifact.ShopCost + " " + Artifact.Currency; //TODO перевод валют на русский
            DescriptionTxt.text = Artifact.Description;
            RaceTxt.text = Artifact.Race.ToString();
        }
    }
}