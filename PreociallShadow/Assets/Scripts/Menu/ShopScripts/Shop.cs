using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Shop;
using Assets.Scripts.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts
{
    internal enum ShopPage
    {
        Altar,
        Artifacts,
        Magic,
        Currency
    }

    internal class Shop : MonoBehaviour
    {
        public CurrencyBar CurrencyBar;

        public ShopPageArtifacts ShopPageArtifacts;
        
        private ShopPage _currentPage;

        private List<CharacterStats> _gameAllCharacters;
        private List<CharacterStats> _gameOwnedCharacters;
        private List<Artifact> _gameAllArtifacts;

        //TODO //private List<Magic> _gameMagic;

        //TODO открыть страницу, на которой были последний раз
        private void Start()
        {
            _gameAllArtifacts = GlobalProgressManager.Instance.GetAllArtifacts();
            OpenShopPage(ShopPage.Artifacts);
        }

        public void OpenShopPage(ShopPage page)
        {
            _currentPage = page;

            switch (page)
            {
                case (ShopPage.Artifacts):
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


        

        public void BuyArtifact(Artifact artifact)
        {
            if (!_gameAllArtifacts.Contains(artifact))
                return;

            int playerLevel = GlobalProgressManager.Instance.GetCurrencyValue(CurrencyName.Level);
            int playerMoney = GlobalProgressManager.Instance.GetCurrencyValue(artifact.Currency);

            if (playerLevel >= artifact.RequiredLevel)
            {
                if (playerMoney >= artifact.ShopCost)
                {
                    //TODO "вы действительно хотите купить этот предмет?
                    GlobalProgressManager.Instance.Spend(artifact.Currency, artifact.ShopCost);
                    artifact.Owned = true;
                    DrawArtifactPage();
                    print("куплен " + artifact.Name);
                }
                else
                {
                    //TODO "у вас не хватает денег! Хотите задонатить?"
                    print("у вас не хватает " + artifact.Currency);
                }

            }
            CurrencyBar.UpdateCurrency();
        }

    }
}
