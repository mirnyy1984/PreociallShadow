using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text TimerText;
    public float RoundLength = 90f;
    
    private enum GameState
    {
        playing,
        lost,
        win,
        draw
    }

    private float _time;
    private GameState _gameState = GameState.playing;
    

    void Start () {
        _time = RoundLength;
	}

	void Update () {
        CountTime();
	}

    private void CountTime()
    {
        if (_gameState == GameState.playing)
        {
            _time -= Time.deltaTime;
            TimerText.text = Mathf.Round(_time).ToString();

            if (_time <= 0)
            {
                Draw();
            }
        }
    }
    
    private void Draw()
    {
        _gameState = GameState.draw;
        print("Draw");
    }

}
