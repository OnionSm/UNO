using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu UI")]
    [SerializeField] private MainMenuUI _main_menu_ui;

    [Header("Event")]
    [SerializeField] private GameEvent _on_load_volume_setting;
    [SerializeField] private FloatEvent _on_change_value_sound;
    [SerializeField] private FloatEvent _on_change_value_sfx;

    void Start()
    {
        LoadComponent();
        PlayBGMSound();
    }

    private void LoadComponent()
    {
        float music_volume = GameManager.Instance.current_sound_volume;
        float sfx_volume = GameManager.Instance.current_sfx_volume;
        AudioManager.Instance.ChangeMusicVolume(music_volume);
        AudioManager.Instance.ChangeSFXVolume(sfx_volume);
    }

    void PlayBGMSound()
    {
        if (AudioManager.Instance.AudioLoaded)
        {
            AudioManager.Instance.PlayBGM(GameManager.Instance.current_music_sound_id);
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
        //StartCoroutine(SwitchSceneCoroutine());
        SceneManager.LoadSceneAsync("SampleScene");

        Debug.Log($"Choose stage: {stage}");
    }

    IEnumerator SwitchSceneCoroutine()
    {

        AsyncOperation loadOp = SceneManager.LoadSceneAsync("SampleScene");
        while (!loadOp.isDone) yield return null;

        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync("MainScene");
        while (!unloadOp.isDone) yield return null;
    }
}
