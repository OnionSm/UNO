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

        AudioClip audioClip = GetAudioClip(audio_id);

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

        AudioClip audioClip = GetAudioClip(audio_id);

        if (audioClip == null)
        {
            Debug.Log($"Can not find audio with id {audio_id} in configs");
            return;
        }
        _audio_source_fx.clip = audioClip;
        _audio_source_fx.Play();
    }

    public AudioClip GetAudioClip(int audio_id)
    {
        foreach (AudioConfig config in _list_audio_configs)
        {
            if (config.id == audio_id)
            {
                return config.audio_clip;
            }
        }
        return null;
    }
}
