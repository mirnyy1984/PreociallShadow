using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Menu.ShopScripts.ShopPages;
using Assets.Scripts.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts
{
    internal enum ShopPageName
    {
        Altar,
        Artifacts,
        Magic,
        Donate
    }

    internal class Shop : MonoBehaviour
    {
        public CurrencyBar CurrencyBar;

        public ShopPageArtifacts ShopPageArtifacts;
        
        private ShopPageName _currentPageName;

        //TODO открыть страницу, на которой были последний раз
        private void Start()
        {
            OpenShopPage(ShopPageName.Artifacts);
        }

        public void OpenShopPage(ShopPageName pageName)
        {
            _currentPageName = pageName;

            switch (pageName)
            {
                case (ShopPageName.Artifacts):
                {
                    DrawArtifactPage();
                    break;
                }
            }

        }

        private void DrawArtifactPage()
        {
            ShopPageArtifacts.gameObject.SetActive(true);
        }


        public void Buy(ShopItem item)
        {
            if (item is Artifact)
            {
                BuyArtifact(item as Artifact);
            }
        }

        public void BuyArtifact(Artifact artifact)
        {
            int playerLevel = GlobalProgressManager.Instance.GetCurrencyValue(CurrencyName.Level);
            int playerMoney = GlobalProgressManager.Instance.GetCurrencyValue(artifact.CurrencyName);

            if (playerLevel >= artifact.RequiredLevel)
            {
                if (playerMoney >= artifact.Cost)
                {
                    //TODO "вы действительно хотите купить этот предмет? да/нет
                    GlobalProgressManager.Instance.Spend(artifact.CurrencyName, artifact.Cost);
                    CurrencyBar.UpdateCurrency();
                    artifact.IsOwned = true;
                    ShopPageArtifacts.DrawPage();
                    print("Куплен артефакт " + artifact.Name);
                }
                else
                {
                    int needed = artifact.Cost - playerMoney;
                    //TODO "У вас не хватает денег! Хотите задонатить?"
                    print("У вас не хватает " + artifact.CurrencyName + "\n" +
                          "Нужно ещё " + needed);
                }

            }
        }

    }
}
