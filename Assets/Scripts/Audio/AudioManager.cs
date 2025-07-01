using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audio_source_bgm;

    public AudioSource AudioSourceBGM
    {
        get { return _audio_source_bgm; }
        set { _audio_source_bgm = value; }
    }

    [SerializeField] private AudioSource _audio_source_fx;
    public AudioSource AudioSourceFX
    {
        get { return _audio_source_fx; }
        set { _audio_source_fx = value; }
    }

    [SerializeField] private List<AudioConfig> _list_audio_configs;
    [SerializeField] private List<AudioConfig> _list_sfx_configs;
    public bool AudioLoaded = false;

    public static AudioManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("More than one Audio Manager");
        }
    }


    private void Start()
    {
        LoadComponent();
    }

    private void LoadComponent()
    {
        this._list_audio_configs = GameManager.Instance.GetAudioConfigs();
        this._list_sfx_configs = GameManager.Instance.GetSFXAudioConfigs();
        AudioLoaded = true;
    }

    public void PlayBGM(int audio_id)
    {
        if (_list_audio_configs == null)
        {
            Debug.Log("There is no config for audio");
            return;
        }
        Debug.Log($"Audio count: {this._list_audio_configs.Count}");

        AudioClip audioClip = GetAudioClip(_list_audio_configs, audio_id);

        if (audioClip == null)
        {
            Debug.Log($"Can not find audio with id {audio_id} in configs");
            return;
        }
        _audio_source_bgm.clip = audioClip;
        _audio_source_bgm.Play();
    }

    public void PlayFX(int audio_id)
    {
        if (_list_audio_configs == null)
        {
            Debug.Log("There is no config for audio");
            return;
        }

        AudioClip audioClip = GetAudioClip(_list_sfx_configs, audio_id);

        if (audioClip == null)
        {
            Debug.Log($"Can not find audio with id {audio_id} in configs");
            return;
        }
        _audio_source_fx.clip = audioClip;
        _audio_source_fx.Play();
    }

    public AudioClip GetAudioClip(List<AudioConfig> list_audio_configs, int audio_id)
    {
        foreach (AudioConfig config in list_audio_configs)
        {
            if (config.id == audio_id)
            {
                return config.audio_clip;
            }
        }
        return null;
    }



    public void ChangeMusicVolume(float value)
    {
        GameManager.Instance.current_sound_volume = value;
        _audio_source_bgm.volume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        GameManager.Instance.current_sfx_volume = value;
        _audio_source_fx.volume = value;
    }
}
