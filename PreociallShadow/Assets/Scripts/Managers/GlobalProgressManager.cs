using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers.PopUpMessage;
using Assets.Scripts.Menu.ShopScripts;
using Assets.Scripts.Stats;
using Assets.Scripts.Stats.Characters;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    internal class GlobalProgressManager : MonoBehaviour
    {
        //Деньги 
        private Dictionary<CurrencyName, int> _currencyValues;

        public static GlobalProgressManager Instance;

        private static List<CharacterBase> _allCharacters = new List<CharacterBase>();
        private static List<Artifact> _allArtifacts = new List<Artifact>();
        private static List<Magic> _allMagics = new List<Magic>();

        #region Singleton
        private void Awake()
        {
            DontDestroyOnLoad(this);
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                DestroyImmediate(gameObject);
            }
        }
        #endregion

        private void Start()
        {
            //TODO //if (firstEnterToGame)
            {
                ResetCurrencyValues();
            }
        }

        private void ResetCurrencyValues()
        {
            _currencyValues = new Dictionary<CurrencyName, int>
            {
                { CurrencyName.Ruby, 0 },
                { CurrencyName.PreociallCrystal, 0 },
                { CurrencyName.Gold, 0 },
                { CurrencyName.Souls, 0 },
                { CurrencyName.DarkCrystal, 0 },
                { CurrencyName.LightCrystal, 0 },

                { CurrencyName.Expirience, 0 },
                { CurrencyName.Level, 0 }
            };
        }

        
    
        public bool Spend(CurrencyName currencyName, int amount)
        {
            if (currencyName == CurrencyName.Expirience)
            {
                AddExp(amount);
            }
            if (currencyName == CurrencyName.Level
                || currencyName == CurrencyName.SkillPoints)
            {
                return false;
            }

            int value = _currencyValues[currencyName];
            if (value - amount < 0)
            {
                //TODO сообщение - недостаточно денег
                print("недостаточно денег");
                return false;
            }
            else
            {
                value -= amount;
                _currencyValues[currencyName] = value;
                return true;
            }
        }

        public void AddCurrency(CurrencyName currencyName, int amount)
        {
            Spend(currencyName, -amount);
            var currencyDisplay = GameObject.FindGameObjectWithTag("CurrencyDisplay");
            currencyDisplay.GetComponent<CurrencyDisplay>().UpdateCurrency();
        }

        
        public void AddExp(int amount)
        {
            int currentExp = _currencyValues[CurrencyName.Expirience];
            int currentLevel = _currencyValues[CurrencyName.Level];

            int newLevelExp = currentExp + amount;
            int newLevel = GameBalanceManager.GetLevelOnExp(newLevelExp);
            newLevel = Mathf.Clamp(newLevel, 0, GameBalanceManager.MaxLevel);

            int maxLevelExp = GameBalanceManager.GetExpForMaxLevel();

            _currencyValues[CurrencyName.Expirience] = Mathf.Clamp(newLevelExp, 0, maxLevelExp);
            
            if(currentLevel != newLevel)
            {
                print("You've got ot level " + newLevel);
                PopUpMessageManager.Instance.ShowMessage("Вы достигли уровня " + newLevel);
                _currencyValues[CurrencyName.Level] = newLevel;
            }
        }
        
        public int GetCurrencyValue(CurrencyName currencyName)
        {
            return _currencyValues[currencyName];
        }

        public List<CharacterBase> GetAllCharacters()
        {
            if (_allCharacters.Count == 0)
            {
                _allCharacters = Resources.LoadAll<CharacterBase>("Characters").ToList();
            }
            return _allCharacters;
        }

        public List<Artifact> GetAllArtifacts()
        {
            if (_allArtifacts.Count == 0)
            {
                _allArtifacts = Resources.LoadAll<Artifact>("Artifacts").ToList();
            }
            return _allArtifacts;
        }

        public List<Magic> GetAllMagic()
        {
            if (_allMagics.Count == 0)
            {
                _allMagics = Resources.LoadAll<Magic>("Magics").ToList();
            }
            return _allMagics;
        }
    }
}