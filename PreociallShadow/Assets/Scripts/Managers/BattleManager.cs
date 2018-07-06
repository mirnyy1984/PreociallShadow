using System.Collections;
using Assets.Scripts.Static;
using Assets.Scripts.Stats;
using Assets.Scripts.Stats.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class BattleManager : MonoBehaviour {

        public Text TimerText;
        public int RoundLength = 90;

        #region Ui top bar public references

        public Slider PlayerHpBar;
        public Image PlayerPortrait;
        public Text PlayerNameText;
        public Text PlayerSkinText;

        public Slider EnemyHpBar;
        public Image EnemyPortrait;
        public Text EnemyNameText;
        public Text EnemySkinText;

        #endregion

        public CharacterBase PlayerCharacter;
        public CharacterBase EnemyCharacter;
        //TODO - получить MaxPlayerHp из PlayerCharacter

        public float MaxPlayerHp;
        public float MaxEnemyHp;
    

        private float _playerHealth;
        private float _enemyHealth;

        private int _playerRoundsWon;
        private int _enemyRondsWon;

        private int _time;
        private GameState _gameState;


        public static BattleManager Instance;

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
                DestroyImmediate(this);
            }
        }
    
        #endregion

        public void Start()
        {
            StartBattle();
        }

        public void StartBattle()
        {
            SetupUiTopBar();

            _playerHealth = MaxPlayerHp;
            _enemyHealth = MaxEnemyHp;
            StartCoroutine(CountTime());
        }

        private void SetupUiTopBar()
        {
            PlayerCharacter = StaticCharacters.Player;
            PlayerPortrait.sprite = PlayerCharacter.Portrait;
            PlayerNameText.text = PlayerCharacter.Name;
            PlayerSkinText.text = PlayerCharacter.SkinName;

            EnemyCharacter = StaticCharacters.Enemy;
            EnemyPortrait.sprite = EnemyCharacter.Portrait;
            EnemyNameText.text = EnemyCharacter.Name;
            EnemySkinText.text = EnemyCharacter.SkinName;
        }

        private enum GameState
        {
            Pause,
            Playing
        }
        private IEnumerator CountTime()
        {
            _time = RoundLength;
            while (_gameState == GameState.Playing)
            {
                _time -= 1;
                TimerText.text = _time.ToString();
                yield return new WaitForSeconds(1);

                if (_time <= 0)
                {
                    Draw();
                }
                yield return null;
            }
            yield break;
        }
    
        private void Draw()
        {
            _gameState = GameState.Pause;
            print("DrawSlot");
        }

        public void DamagePlayer(float damage)
        {
            _playerHealth -= damage;
            EnemyHpBar.value = _enemyHealth / MaxPlayerHp;
            if (_playerHealth <= 0)
            {
                _gameState = GameState.Pause;
                print("You lost");
            }
        }

        public void DamageEnemy(float damage)
        {
            _enemyHealth -= damage;
            EnemyHpBar.value = _enemyHealth / MaxEnemyHp;
            if (_playerHealth <= 0)
            {
                _gameState = GameState.Pause;
                print("You won");
            }
        }
    }
}
