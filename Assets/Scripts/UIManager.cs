
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    
    [SerializeField] Slider _slider;
    [SerializeField] GameObject _congratsPanel;
    [SerializeField] Image _nextLevelBGImage;
    [SerializeField] LevelBarColorsSO levelBarColorsData;
    public void UpdateLevelBar(float collected, float total)
    {
        _slider.value = (collected / total) * 100f;

        if (_slider.value == 100f)
        {
            
            _nextLevelBGImage.color = levelBarColorsData.FillColor;
            
            GameManager.Instance.SetGameStatus(GameState.PreFinish);

            //say player make radius 1-to-10
            //rotate ground obj 

            //FinishGameAnim();

            //SetCongratsPanel(true);
        }
        else
        {
            _nextLevelBGImage.color = levelBarColorsData.DefaultColor;
        }
    }

    public void SetCongratsPanel(bool isShow) => _congratsPanel.SetActive(isShow);

}
