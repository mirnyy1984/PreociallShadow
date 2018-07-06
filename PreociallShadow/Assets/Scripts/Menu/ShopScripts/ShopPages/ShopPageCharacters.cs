using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Stats;
using Assets.Scripts.Stats.Characters;
using UnityEngine;

namespace Assets.Scripts.Menu.ShopScripts.ShopPages
{
    internal class ShopPageCharacters : ShopPage
    {
        [SerializeField] private List<CharacterBase> _characters = new List<CharacterBase>();

        private void Start()
        {
            _characters = GlobalProgressManager.Instance.GetAllCharacters();
            //TODO TEST
            DrawPage();
        }

        //Вывзывается верхней панелью фильтров в магазине
        public override List<ShopItem> SortItems()
        {
            List<CharacterBase> sortedCharacters = new List<CharacterBase>(_characters);
            
            //Порядок сортировки
            switch (SortOrderDropdown.value)
            {
                //Сортировка по Уровню
                case (0):
                {
                    sortedCharacters.Sort((x, y) => x.RequiredLevel.CompareTo(y.RequiredLevel));
                    break;
                }
                //Сортировка по Названию
                case (1):
                {
                    sortedCharacters.Sort((x, y) => String.Compare(x.Name, y.Name, StringComparison.Ordinal));
                    break;
                }
                //Сортировка по Цене
                case (2):
                {
                    //TODO ещё учитывать валюту
                    sortedCharacters.Sort((x, y) => x.Cost.CompareTo(y.Cost));
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
                    sortedCharacters = sortedCharacters.Where(x => x.Race == Race.Human).ToList();
                    break;
                }
                //Антропоморфы
                case (2):
                {
                    sortedCharacters = sortedCharacters.Where(x => x.Race == Race.Anthrophomorph).ToList();
                    break;
                }
                //Инопланетяне
                case (3):
                {
                    sortedCharacters = sortedCharacters.Where(x => x.Race == Race.BadAlien
                                                                 || x.Race == Race.GoodAlien).ToList();
                    break;
                }
            }

            //Развернём список по кнопке справа
            if (SortOrder)
            {
                sortedCharacters.Reverse();
            }
            List<ShopItem> sortedItems = sortedCharacters.Cast<ShopItem>().ToList();
            return sortedItems;
        }
    }
}
