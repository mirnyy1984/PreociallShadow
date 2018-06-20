using System.Collections.Generic;
using Assets.Scripts.Shop;
using Assets.Scripts.Stats;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    internal class GlobalProgressManager : MonoBehaviour
    {
        public const int MaxLevel = 50; //Максимальный уровань в игре
        public const float LevelExpDelta = 0.2f; //насколько больше (в процентах) опыта нужно на каждом уровне.
        //опыт_для_следующего_уровня = опыт_для_этого_уровня + опыт_для_этого_уровня * LevelDelta; 
        public const int ExpToFirstLevel = 100;

        public GameObject GameArtifactsAll; //Список всех артефактов игры
        //TODO //public GameObject GameMagicAll; //Список всех магий игры
        public GameObject GameCharactersAll; //Список всех персонажей игры
        public GameObject GameCharactersOwned; //Список купленных персонажей игры
        
        private int[] _levelsExp; //Количество опыта, необходимое для каждого уровня

        //Деньги 
        private Dictionary<CurrencyName, int> _currencyValues;
    
        public static GlobalProgressManager Instance;

        private void Awake()
        {
            #region Singleton
            DontDestroyOnLoad(this);
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                DestroyImmediate(this);
            }
            #endregion

            //TODO //if (firstEnterToGame)
            {
                CalculateLevelsExp();
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

        private void CalculateLevelsExp()
        { 
            _levelsExp = new int[MaxLevel];
            _levelsExp[0] = 0;
            _levelsExp[1] = ExpToFirstLevel;

            for (int i = 2; i < MaxLevel; i++)
            {
                _levelsExp[i] = Mathf.RoundToInt(_levelsExp[i - 1] + _levelsExp[i - 1] * LevelExpDelta);
            }

            //print("Распредениелие опыта по уровням:");
            //for (int i = 0; i < MaxLevel; i++) print(_levelsExp[i]);
        }
    
        public bool Spend(CurrencyName currencyName, int amount)
        {
            if (currencyName == CurrencyName.Expirience
                || currencyName == CurrencyName.Level
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

        public void Add(CurrencyName currencyName, int amount)
        {
            if (currencyName == CurrencyName.Expirience)
            {
                AddExp(amount);
                return;
            }
            Spend(currencyName, -amount);
        }

        public void AddExp(int amount)
        {
            int currentLevel = GetCurrentLevel();

            if (currentLevel >= MaxLevel)
            {
                _currencyValues[CurrencyName.Expirience] = _levelsExp[MaxLevel - 1];
                return;
            }

            if (_currencyValues[CurrencyName.Expirience] + amount >= _levelsExp[MaxLevel - 1])
            {
                _currencyValues[CurrencyName.Expirience] = _levelsExp[MaxLevel - 1];
                return;
            }

            _currencyValues[CurrencyName.Expirience] += amount;
            int newLevel = GetCurrentLevel();


            if (currentLevel != newLevel)
            {
                //TODO you've leveled up message
                _currencyValues[CurrencyName.Level] = newLevel;
            }
        }

        public int GetCurrentLevel()
        {
            int currentExp = _currencyValues[CurrencyName.Expirience];

            if (currentExp >= _levelsExp[MaxLevel - 1])
            {
                _currencyValues[CurrencyName.Level] = MaxLevel;
                return MaxLevel;
            }

            for (int i = 0; i < MaxLevel - 1; i++)
            {
                if (currentExp >= _levelsExp[i] &&
                    currentExp < _levelsExp[i+1])
                {
                    _currencyValues[CurrencyName.Level] = i;
                    return i;
                }
            }
            return 0;
        }

        public void _TEST_ADD_30000_EXP()
        {
            AddExp(30000);
            GetLevelProgress();
        }

        //Возвращает прогресс на текущем уровне в диапазоне [0.0, 1.0] 
        public float GetLevelProgress()
        {
            int currentExp = _currencyValues[CurrencyName.Expirience];
            int currentLvl = GetCurrentLevel();

            if (currentLvl == MaxLevel)
            {
                return 1f;
            }

            int expNeeded = _levelsExp[currentLvl + 1]; //Сколько всего опыта нужно до следующего

            int expToThisLevel = _levelsExp[currentLvl]; //Сколько опыта нужно для этого уровня
            int expDelta = expNeeded - expToThisLevel;  //Сколько опыта между следующим и нашим уровнем
            int expRemaining = expNeeded - currentExp; //Сколько опыта осталось для след уровня
            int progressOnCurrLevel = expDelta - expRemaining;
            /*
        Debug.Log("currentLvl " + currentLvl);
        Debug.Log("currentExp " + currentExp);
        Debug.Log("exp to next " + _levelsExp[currentLvl + 1]);
        Debug.Log("exp to this " + _levelsExp[currentLvl]);
        Debug.Log("currentExp " + currentExp);
        Debug.Log("progressOnCurrLevel " + progressOnCurrLevel);
        Debug.Log("LevelProgress " + (float)progressOnCurrLevel / (float)expDelta);
        */
            return (float)progressOnCurrLevel / (float)expDelta;
        }

        public int GetCurrencyValue(CurrencyName Name)
        {
            return _currencyValues[Name];
        }


        public List<CharacterStats> GetOwnedCharacters()
        {
            List<CharacterStats> ownedCharacters = new List<CharacterStats>();
            foreach (Transform child in GameCharactersOwned.transform)
            {
                ownedCharacters.Add(child.GetComponent<CharacterStats>());
            }

            return ownedCharacters;
        }

        public List<CharacterStats> GetAllCharacters()
        {
            List<CharacterStats> allCharacters = new List<CharacterStats>();
            foreach (Transform child in GameCharactersAll.transform)
            {
                allCharacters.Add(child.GetComponent<CharacterStats>());
            }

            return allCharacters;
        }

        public List<Artifact> GetAllArtifacts()
        {
            List<Artifact> ownedArtifacts = new List<Artifact>();
            foreach (Transform child in GameArtifactsAll.transform)
            {
                ownedArtifacts.Add(child.GetComponent<Artifact>());
            }

            return ownedArtifacts;
        }


    }
}