using System;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.ShopScripts
{
    class ShopPageArtifacts : MonoBehaviour
    {
        public Transform ShopContent;
        
        public Dropdown RaceFilterDropdown;
        public Dropdown SortOrderDropdown;
        public Toggle HideOwnedToggle;
        public bool SortOrder; //descending ascending

        public GameObject ArtifactSlotPrefab;

        private List<Artifact> _gameAllArtifacts;


        private void OnEnable()
        {
            _gameAllArtifacts = GlobalProgressManager.Instance.GetAllArtifacts();
            DrawPage();
        }

        private void DrawPage()
        {
            foreach (Transform child in ShopContent.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (var artifact in _gameAllArtifacts)
            {
                //TODO if show owned
                if (!artifact.Owned)
                {
                    var slotGo = Instantiate(ArtifactSlotPrefab, ShopContent);
                    var slotScript = slotGo.GetComponent<ArtifactShopSlot>();
                    slotScript.SetItemToDisplay(artifact);
                    //TODO проверить доступен ли слот
                    slotScript.DrawAvailableSlot();
                }
            }
        }

        //Это вызывает dropdown сортировки
        public void SortItems()
        {//todo
            _gameAllArtifacts.Sort((x, y) => x.RequiredLevel.CompareTo(y.RequiredLevel));
            DrawPage();
        }

        public void Buy(Artifact artifact)
        {
            if (!_gameAllArtifacts.Contains(artifact))
            {
                print("Произошла какая-то ошибка. Артефакта, который вы хотите купить, не существует.");
                return;
            }
        }


    }
}
