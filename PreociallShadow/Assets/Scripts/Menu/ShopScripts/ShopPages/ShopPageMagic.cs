using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Stats;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts.ShopPages
{
    internal class ShopPageMagic : ShopPage
    {
        
        public Dropdown RaceFilterDropdown;
        public Dropdown SortOrderDropdown;
        public Toggle HideOwnedToggle;
        private List<Magic> _magicList = new List<Magic>();

        private void Start()
        {
            _magicList = GlobalProgressManager.Instance.GetAllMagic();
        }

        //Вывзывается верхней панелью фильтров в магазине
        public override List<ShopItem> SortItems()
        {
            List<Magic> sortedArtifacts = new List<Magic>(_magicList);

            sortedArtifacts = sortedArtifacts.Where(x => x.IsBuyable).ToList();
            if (HideOwnedToggle.isOn)
            {
                sortedArtifacts = sortedArtifacts.Where(x => !x.IsOwned).ToList();
            }

            //Порядок сортировки
            switch (SortOrderDropdown.value)
            {
                //Сортировка по Уровню
                case (0):
                    {
                        sortedArtifacts.Sort((x, y) => x.RequiredLevel.CompareTo(y.RequiredLevel));
                        break;
                    }
                //Сортировка по Названию
                case (1):
                    {
                        sortedArtifacts.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
                        break;
                    }
                //Сортировка по Цене
                case (2):
                    {
                        sortedArtifacts.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                        break;
                    }
                //Сортировка по Урону
                case (3):
                    {
                        sortedArtifacts.Sort((x, y) => x.Damage.CompareTo(y.Damage));
                        break;
                    }
            }

            //Фильтр по расе
            switch (RaceFilterDropdown.value)
            {
                //Все
                case (0):
                    {

                        break;
                    }
                //Земляне
                case (1):
                    {
                        sortedArtifacts = sortedArtifacts.Where(x => x.Race == Race.Human).ToList();
                        break;
                    }
                //Антропоморфы
                case (2):
                    {
                        sortedArtifacts = sortedArtifacts.Where(x => x.Race == Race.Anthrophomorph).ToList();
                        break;
                    }
                //Инопланетяне
                case (3):
                    {
                        sortedArtifacts = sortedArtifacts.Where(x => x.Race == Race.BadAlien
                                                                     || x.Race == Race.GoodAlien).ToList();
                        break;
                    }
            }

            //Развернём список по кнопке справа
            if (SortOrder)
            {
                sortedArtifacts.Reverse();
            }
            List<ShopItem> sortedItems = sortedArtifacts.Cast<ShopItem>().ToList();
            return sortedItems;
        }
    }
}
