using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Stats;
using UnityEngine;

namespace Assets.Scripts.Menu.ShopScripts.ShopPages
{
    internal class ShopPageArtifacts : ShopPage
    {
        [SerializeField] private List<Artifact> _artifacts = new List<Artifact>();

        private void Start()
        {
            _artifacts = GlobalProgressManager.Instance.GetAllArtifacts();
        }

        //Вывзывается верхней панелью фильтров в магазине
        public override List<ShopItem> SortItems()
        {
            List<Artifact> sortedArtifacts = new List<Artifact>(_artifacts);
            
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
                    sortedArtifacts.Sort((x, y) => String.Compare(x.Name, y.Name, StringComparison.Ordinal));
                    break;
                }
                //Сортировка по Цене
                case (2):
                {
                    //TODO ещё учитывать валюту
                    sortedArtifacts.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                    break;
                }
                //Сортировка по Бонусу к урону
                case (3):
                {
                    //TODO сортировать по уровню, группировать по урону
                    sortedArtifacts.Sort((x, y) => x.DamageAddBonus.CompareTo(y.DamageAddBonus));
                    break;
                }
                //Сортировка по Бонусу к здоровью
                case (4):
                {
                    //TODO сортировать по уровню, группировать по здоровью
                    sortedArtifacts.Sort((x, y) => x.HealthAddBonus.CompareTo(y.HealthAddBonus));
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

            List<ShopItem> sortedItems = sortedArtifacts.Cast<ShopItem>().ToList();
            return sortedItems;
        }
    }
}
