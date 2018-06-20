using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Static;
using Assets.Scripts.Stats;
using UnityEngine;



//todo ДОБАВИТЬ СОРТИРОВКУ ЭКРАНА ВЫБОРА ПЕРСОНАЖЕЙ. ДОБАВИТЬ ЗАПОМИНАНИЕ ВЫБОРА ИЗ ПОСЛЕДНЕГО БОЯ

namespace Assets.Scripts.Menu
{
    public class FighterSelectScreen : MonoBehaviour
    {
        public Transform CharactersContent;

        public GameObject FighterSlotPrefab;

        private GameObject _selectedFighterPreviev;
        private CharacterStats _selectedFighterStats;

        private List<CharacterStats> _avalableCharacters;

        private void Start()
        {
            _avalableCharacters = GlobalProgressManager.Instance.GetOwnedCharacters();

            foreach (var characterStats in _avalableCharacters)
            {
                var slotGo = Instantiate(FighterSlotPrefab, CharactersContent.transform);
                var slotScript = slotGo.GetComponent<FighterSelectSlot>();
                slotScript.SelectScreen = this;
                slotScript.SetCharacterStats(characterStats);
            }
            if (_avalableCharacters.Count != 0)
                SelectFighter(_avalableCharacters[0]);
            StartCoroutine(Rotate());
        }

        public void SelectFighter(CharacterStats stats)
        {
            _selectedFighterStats = stats;
            Destroy(_selectedFighterPreviev);
            _selectedFighterPreviev = Instantiate(stats.CharacterPrefab, transform.position, transform.rotation, transform);
        }

        private IEnumerator Rotate()
        {
            while (true)
            {
                transform.Rotate(transform.up, 1);
                yield return null;
            }
        }

        //Вызывает кнопка "в бой"
        public void OnStartBattleClick()
        {
            Debug.Log("Starting battle with " + _selectedFighterStats.GetComponent<CharacterStats>().Name);
            StaticCharacterStats.Player = _selectedFighterStats;
            //TODO выбор врага, соответственно уровню игрока и сложности.
            StaticCharacterStats.Enemy = GlobalProgressManager.Instance.GetAllCharacters()[0];
            LoadingScreen.Instance.LoadLevel(1);
        }

    }
}
