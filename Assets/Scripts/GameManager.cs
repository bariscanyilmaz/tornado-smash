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
    private float _collected;
    private float _total;
    private GameState _gameStatus;
    public float Total { get => _total; }
    public float Collected { get => _collected; }
    public GameState GameStatus { get => _gameStatus; }

    [SerializeField] GameObject _congratsPanel;
    [SerializeField] Slider _slider;
    [SerializeField] GameObject _nextLevelBG;
    Color _defaultColor = new Color(0.8509804f, 0.7960784f, 0.7960784f, 1f);
    Color _fillColor = new Color(0.7058949f, 0.8584906f, 0.1093361f, 1f);

    WaitForSeconds wait = new WaitForSeconds(.001f);
    void Start()
    {
        InitGame();
    }
    public void StartGame()
    {
        _gameStatus = GameState.Play;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void InitGame()
    {
        SetCongratsPanel(false);
        _gameStatus = GameState.Play;
        _collected = 0;
        _total = FindObjectsOfType<Obstacle>().Length;
        UpdateLevelBar();

    }

    public void IncreaseCollected()
    {
        _collected++;

        UpdateLevelBar();
    }

    void UpdateLevelBar()
    {
        _slider.value = (_collected / _total) * 100f;

        if (_slider.value == 100f)
        {
            //set color;
            _nextLevelBG.GetComponent<Image>().color = _fillColor;

            //SetCongratsPanel(true);
            _gameStatus = GameState.PreFinish;

            //say player make radius 1-to-10
            //rotate ground obj 
            //
            //FinishGameAnim();


        }
        else
        {
            _nextLevelBG.GetComponent<Image>().color = _defaultColor;
        }
    }

    void SetCongratsPanel(bool isShow)
    {
        _congratsPanel.SetActive(isShow);
    }


    
}
