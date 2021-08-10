
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Color _color;
    [SerializeField] Slider _slider;
    [SerializeField] GameObject _congratsPanel;
    [SerializeField] Image _nextLevelBGImage;


    Color _defaultColor = new Color(0.8509804f, 0.7960784f, 0.7960784f, 1f);
    Color _fillColor = new Color(0.7058949f, 0.8584906f, 0.1093361f, 1f);
    


    public void UpdateLevelBar(float collected, float total)
    {
        _slider.value = (collected / total) * 100f;

        if (_slider.value == 100f)
        {
            
            _nextLevelBGImage.color = _fillColor;
            SetCongratsPanel(true);
            GameManager.Instance.SetGameStatus(GameState.PreFinish);

            //say player make radius 1-to-10
            //rotate ground obj 

            //FinishGameAnim();
        }
        else
        {
            _nextLevelBGImage.color = _defaultColor;
        }
    }

    public void SetCongratsPanel(bool isShow) => _congratsPanel.SetActive(isShow);

}
