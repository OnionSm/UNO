using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _setting_panel;
    [SerializeField] private Slider _music_slider;
    [SerializeField] private Slider _sfx_slider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSettingPanel() 
    {
        _setting_panel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseSettingPanel()
    {
        Time.timeScale = 1f;
        _setting_panel?.SetActive(false);
    }

    public void SetUIMusicSlider(float value)
    {
        _music_slider.value = value;
    }

    public void SetUISFXSlider(float value)
    {
        _sfx_slider.value = value;
    }
}
