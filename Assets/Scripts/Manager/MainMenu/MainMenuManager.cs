using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu UI")]
    [SerializeField] private MainMenuUI _main_menu_ui;

    [SerializeField] private int _bgm_sound_id = 0;
    [SerializeField] private float _init_music_volume = 0.5f;
    [SerializeField] private float _init_sfx_music = 0.5f;

    private int _sfx_sound_id = 0;

    void Start()
    {
        LoadComponent();
        PlayBGMSound();
        SetInitVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void LoadComponent()
    {

    }
    void PlayBGMSound()
    {
        if (AudioManager.Instance.AudioLoaded)
        {
            AudioManager.Instance.PlayBGM(_bgm_sound_id);
        }
        else
        {
            Invoke("PlayBGMSound", 0.5f);
        }
    }

    public void ChooseStage(int stage)
    {
        GameManager.Instance.current_stage = stage;
        GameManager.Instance.SetStageConfig();
        SceneManager.LoadSceneAsync("SampleScene");

        Debug.Log($"Choose stage: {stage}");
    }

    public void SetInitVolume()
    {
        AudioManager.Instance.ChangeMusicVolume( _init_music_volume );
        AudioManager.Instance.ChangeSFXVolume( _init_sfx_music );
        _main_menu_ui.SetUIMusicSlider(_init_music_volume);
        _main_menu_ui.SetUISFXSlider(_init_sfx_music);  
    }
    public void PlaySFXSound()
    {

    }
}
