using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    Play,
    Pause,
    PreFinish
}

public class GameManager : Singleton<GameManager>
{
    float _collected;
    float _total;
    GameState _gameStatus;

    public float Total => _total;
    public float Collected => _collected;
    public GameState GameStatus => _gameStatus;

    void Start()
    {

        UIManager.Instance.SetCongratsPanel(false);
        _gameStatus = GameState.Play;
        _collected = 0;
        _total = FindObjectsOfType<Obstacle>().Length;
        UIManager.Instance.UpdateLevelBar(_collected, _total);
    }

    public void StartGame() => _gameStatus = GameState.Play;

    public void RestartGame() =>SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void IncreaseCollected() => _collected++;
    public void SetGameStatus(GameState gameState) => _gameStatus = gameState;

}

