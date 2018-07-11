using System;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.Dialog;
using Assets.Scripts.Menu.ShopScripts.ShopPages;
using Assets.Scripts.Stats;
using Assets.Scripts.Stats.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts
{
    internal class Shop : MonoBehaviour
    {
        public static Shop Instance;
        public CurrencyDisplay CurrencyDisplay;
        public ShopPageArtifacts ShopPageArtifacts;
        public ShopPageCharacters ShopPageCharacters;
        public ShopPageMagic ShopPageMagic;
        //TODO ShopPageDonate

        private ScrollRect _scrollView;

        public Button AltarButton;
        public Button ArtifactsButton;
        public Button MagicButton;
        public Button DonateButton;

        private GlobalProgressManager _globalProgressManager;
        private ConfirmDialogManager _dialogManager;
        private ShopItem _lastSelectedItem;
       [SerializeField] private ShopPage _currentPage;

        private List<ShopPage> _shopPages;
        private List<Button> _shopButtons;

        //TODO открыть страницу, на которой были последний раз
        private void Start()
        {
            _shopPages = new List<ShopPage>(
                new ShopPage[] {ShopPageCharacters, ShopPageArtifacts, ShopPageMagic });
                                                                            
            _shopButtons = new List<Button>(
                new Button[] {AltarButton, ArtifactsButton, MagicButton, DonateButton});

            _scrollView = GetComponentInChildren<ScrollRect>();
            _globalProgressManager = GlobalProgressManager.Instance;
            _dialogManager = ConfirmDialogManager.Instance;
            OpenShopPage(0);
        }

        #region Singleton

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);
        }

        #endregion

        //Это для кнопок
        public void OpenShopPage(int page)
        {
            _currentPage = _shopPages[page];
            _scrollView.content = _currentPage.ShopContent;

            int pagesN = Enum.GetValues(typeof(ShopPageName)).Length;

            for (int i = 0; i < pagesN; i++)
            {
                //TODO за неимением третьей чётвёртой страницы
                if (i == 3 || i == 2) continue;

                if (i == page)
                {
                    _shopPages[i].EnableContent();
                    _shopButtons[i].interactable = false;
                }
                else
                {
                    _shopPages[i].DisableContent();
                    _shopButtons[i].interactable = true;
                }
            }

            if (page == (int) ShopPageName.Altar)
            {
                _scrollView.horizontal = true;
                _scrollView.vertical = false;
            }
            else
            {
                _scrollView.horizontal = false;
                _scrollView.vertical = true;
            }
        }

        public void Buy(ShopItem item)
        {
            _lastSelectedItem = item;

            int playerLevel = _globalProgressManager.GetCurrencyValue(CurrencyName.Level);
            int playerMoney = _globalProgressManager.GetCurrencyValue(item.CurrencyName);

            if (playerLevel >= item.RequiredLevel)
            {
                if (playerMoney >= item.Cost)
                {
                    string messageText = "Купить \"" + item.Name + "\" за "
                                         + item.Cost + " " + item.CurrencyName + "?";
                    print("Пробуем купить " + _lastSelectedItem.Name);
                    _dialogManager.ConfirmDialogYN(ConfirmPurchaseLastSelectedItem, messageText); //Показать окно подтверждения
                }
                else
                {
                    int needed = item.Cost - playerMoney;
                    //TODO "У вас не хватает денег! Хотите задонатить?"
                    print("У вас не хватает " + item.CurrencyName + "\n" +
                          "Нужно ещё " + needed);
                }

            }
        }

        private void ConfirmPurchaseLastSelectedItem()
        {
            print("Пробуем купить 2 " + _lastSelectedItem.Name);
            _globalProgressManager.Spend(_lastSelectedItem.CurrencyName, _lastSelectedItem.Cost);
            CurrencyDisplay.UpdateCurrency();
            _lastSelectedItem.IsOwned = true;
            _currentPage.DrawPage();
            print("Куплен " + _lastSelectedItem.Name);
            
            if (_lastSelectedItem is CharacterBase)
            {
                var character = _lastSelectedItem as CharacterBase;
                character.OwnedStats = new OwnedCharacterStats();
            }
        }


        public void OnFilterButtonClick()
        {
            _currentPage.ShowFilterWindow();
        }
    }
}
