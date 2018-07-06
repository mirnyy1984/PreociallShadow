using Assets.Scripts.Managers;
using Assets.Scripts.Managers.Dialog;
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
        public static Shop Instance;
        public CurrencyDisplay CurrencyDisplay;
        public ShopPageArtifacts ShopPageArtifacts;
        public ShopPageCharacters ShopPageCharacters;
        public ShopPageMagic ShopPageMagic;
        //--
        //--

        public Button AltarButton;
        public Button ArtifactsButton;
        public Button MagicButton;
        public Button DonateButton;

        private GlobalProgressManager _globalProgressManager;
        private ConfirmDialogManager _dialogManager;
        private ShopItem _lastSelectedItem;
        private ShopPageName _currentPageName;
        private ShopPage _currentPage;

        //TODO открыть страницу, на которой были последний раз
        private void Start()
        {
            _globalProgressManager = GlobalProgressManager.Instance;
            _dialogManager = ConfirmDialogManager.Instance;
            OpenShopPage(ShopPageName.Altar);
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
            ShopPageName spn = (ShopPageName)page;
            OpenShopPage(spn);
        }

        //Эту функцию кнопки вызвать не могут потому что не понимают enum-ы
        public void OpenShopPage(ShopPageName pageName)
        {
            _currentPageName = pageName;

            switch (pageName)
            {
                case (ShopPageName.Altar):
                {
                        DrawAltarPage();
                    break;
                }
                case (ShopPageName.Artifacts):
                {
                    DrawArtifactPage();
                    break;
                }
                case (ShopPageName.Magic):
                {
                    //DrawMagicPage();
                    break;
                }
                case (ShopPageName.Donate):
                {
                    //DrawDonatePage();
                    break;
                }
            }

        }

        private void DrawAltarPage()
        {
            _currentPage = ShopPageCharacters;

            ShopPageCharacters.EnableContent();
            ShopPageArtifacts.DisableContent();
            AltarButton.interactable = false;
            ArtifactsButton.interactable = true;
            //ShopPageMagic.gameObject.SetActive(true);
        }

        private void DrawArtifactPage()
        {
            _currentPage = ShopPageArtifacts;

            ShopPageCharacters.DisableContent();
            ShopPageArtifacts.EnableContent();
            ArtifactsButton.interactable = false;
            AltarButton.interactable = true;
            //ShopPageArtifacts.gameObject.SetActive(true);
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
            _globalProgressManager.Spend(_lastSelectedItem.CurrencyName, _lastSelectedItem.Cost);
            CurrencyDisplay.UpdateCurrency();
            _lastSelectedItem.IsOwned = true;
            _currentPage.DrawPage();
            print("Куплен " + _lastSelectedItem.Name);
        }


        public void OnFilterButtonClick()
        {
            _currentPage.ShowFilterWindow();
        }
    }
}
