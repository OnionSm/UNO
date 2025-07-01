using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfigs", menuName = "Audio/AudioConfigs")]
public class AudioConfigs : ScriptableObject
{
    public List<AudioConfig> _all_audio_configs;
}

[System.Serializable]
public class AudioConfig
{
    public int id;
    public string name;
    public AudioClip audio_clip;

    public AudioConfig Clone()
    {
        return new AudioConfig
        {
            id = this.id,
            name = this.name,
            audio_clip = this.audio_clip
        };
    }
}
