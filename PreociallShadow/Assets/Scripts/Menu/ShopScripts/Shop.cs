using System;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.PopUpMessageManager;
using Assets.Scripts.Menu.ShopScripts.ShopPages;
using Assets.Scripts.Stats;
using UnityEditor.Experimental.Build.AssetBundle;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.WSA;

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
        public static Shop Instance;
        public CurrencyBar CurrencyBar;
        public ShopPageArtifacts ShopPageArtifacts;
        private GlobalProgressManager _globalProgressManager;
        private ConfirmDialogManager _dialogManager;
        private ShopItem _lastSelectedItem;
        private ShopPageName _currentPageName;

        //TODO открыть страницу, на которой были последний раз
        private void Start()
        {
            _globalProgressManager = GlobalProgressManager.Instance;
            _dialogManager = ConfirmDialogManager.Instance;
            OpenShopPage(ShopPageName.Artifacts);
        }

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);
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
            _lastSelectedItem = item;
            if (item is Artifact)
            {
                BuyArtifact(item as Artifact);
            }
        }

        public void BuyArtifact(Artifact artifact)
        {
            int playerLevel = _globalProgressManager.GetCurrencyValue(CurrencyName.Level);
            int playerMoney = _globalProgressManager.GetCurrencyValue(artifact.CurrencyName);

            if (playerLevel >= artifact.RequiredLevel)
            {
                if (playerMoney >= artifact.Cost)
                {
                    string messageText = "Купить " + artifact.Name + " за " + artifact.Cost + " " +
                                         artifact.CurrencyName + "?";
                    _dialogManager.ConfirmDialogYN(ConfirmPurchaseLastSelectedItem, messageText); //Показать окно подтверждения
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
        private void ConfirmPurchaseLastSelectedItem()
        {
            _globalProgressManager.Spend(_lastSelectedItem.CurrencyName, _lastSelectedItem.Cost);
            CurrencyBar.UpdateCurrency();
            _lastSelectedItem.IsOwned = true;
            ShopPageArtifacts.DrawPage();
            print("Куплен " + _lastSelectedItem.Name);
        }
    }
}
