using UnityEngine;
using UnityEngine.SceneManagement;
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
    int _levelIndex;
    GameState _gameStatus;
    GameObject _currentLevel;

    [SerializeField] GameObject[] _levelPrefabs;

    public float Total => _total;
    public float Collected => _collected;
    public GameState GameStatus => _gameStatus;

    void Start()
    {

        LoadNextLevel();
        StartGame();
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void IncreaseCollected() => _collected++;
    public void IncreaseLevel() => _levelIndex++;
    public void SetGameStatus(GameState gameState) => _gameStatus = gameState;
    public void StartGame()
    {
        _gameStatus = GameState.Play;
        _collected = 0;
        _total = FindObjectsOfType<Obstacle>().Length;
        UIManager.Instance.UpdateLevelBar(_collected, _total);
    }

    public void LoadNextLevel()
    {

        if (_currentLevel) Destroy(_currentLevel);
        _currentLevel = Instantiate(_levelPrefabs[_levelIndex % _levelPrefabs.Length], Vector3.zero, Quaternion.identity);
    }

}

