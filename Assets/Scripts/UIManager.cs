using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    
    [SerializeField] float _delayTime=1f;
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
            Invoke("SetStatus",_delayTime);
        }
        else
        {
            _nextLevelBGImage.color = levelBarColorsData.DefaultColor;
        }
    }

    public void SetCongratsPanel(bool isShow) => _congratsPanel.SetActive(isShow);
    void SetStatus()=>GameManager.Instance.SetGameStatus(GameState.PreFinish);

}
