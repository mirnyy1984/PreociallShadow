using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Static;
using Assets.Scripts.Stats;
using Assets.Scripts.Stats.Characters;
using UnityEngine;



//todo ДОБАВИТЬ СОРТИРОВКУ ЭКРАНА ВЫБОРА ПЕРСОНАЖЕЙ. ДОБАВИТЬ ЗАПОМИНАНИЕ ВЫБОРА ИЗ ПОСЛЕДНЕГО БОЯ

namespace Assets.Scripts.Menu
{
    public class FighterSelectScreen : MonoBehaviour
    {
        public Transform CharactersContent;

        public GameObject FighterSlotPrefab;

        private GameObject _selectedFighterPreviev;
        private CharacterBase _selectedFighter;

        private List<CharacterBase> _avalableCharacters;

        private void Start()
        {
            //TODO _avalableCharacters = GlobalProgressManager.Instance.GetOwnedCharacters();

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

        public void SelectFighter(CharacterBase stats)
        {
            _selectedFighter = stats;
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
            //TODO //Debug.Log("Starting battle with " + _selectedFighter.GetComponent<Character>().Name);
            StaticCharacters.Player = _selectedFighter;
            //TODO выбор врага, соответственно уровню игрока и сложности.
            StaticCharacters.Enemy = GlobalProgressManager.Instance.GetAllCharacters()[0];
            LoadingScreenManager.Instance.LoadLevel(1);
        }

    }
}
