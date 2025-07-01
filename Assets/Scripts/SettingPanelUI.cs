using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingPanelUI : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider _music_slider;
    [SerializeField] private Slider _sfx_slider;

    [SerializeField] private FloatEvent _on_change_value_sound;
    [SerializeField] private FloatEvent _on_change_value_sfx;
    [SerializeField] private IntEvent _on_sound_click;

    private void Start()
    {
        
    }

    //public void LoadVolumeSetting()
    //{
    //    Debug.Log("Volume Setting");
    //    float music_volume = GameManager.Instance.current_sound_volume;
    //    float sfx_volume = GameManager.Instance.current_sfx_volume;
    //    AudioManager.Instance.ChangeMusicVolume(music_volume);
    //    AudioManager.Instance.ChangeSFXVolume(sfx_volume);
    //    _music_slider.value = music_volume;
    //    _sfx_slider.value = sfx_volume;
    //}
    public void ChangeSoundVolume(float volume)
    {
        _on_change_value_sound?.RaiseEvent(volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        _on_change_value_sfx?.RaiseEvent(volume);
    }

    public void PlayClickSound(int value)
    {
        _on_sound_click?.RaiseEvent(value);
    }
    public void OpenPanel()
    {
        gameObject.SetActive(true);
        _music_slider.value = GameManager.Instance.current_sound_volume;
        _sfx_slider.value = GameManager.Instance.current_sfx_volume;
        Time.timeScale = 0f;
    }
    public void ClosePanel()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
