using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardConfigs _card_configs;
    [SerializeField] private DeckConfigs _deck_configs;
    [SerializeField] private ColorConfigs _color_configs;
    [SerializeField] private AudioConfigs _audio_configs;
    [SerializeField] private StageConfigs _stage_configs;
    public int current_stage { get; set; } = 0;
    public StageConfig current_stage_config { get; set; }

    public static GameManager Instance;

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
            Debug.Log("More than one GameManager");
        }
    }
    public List<CardConfig> GetListCardConfigs()
    {
        return _card_configs.configs;
    }

    public Sprite GetProtecter()
    {
        return _card_configs._card_protecter;
    }

    public List<CardDeck> GetDecks()
    {
        return _deck_configs._deck_configs;
    }
    public List <ColorConfig> GetColorConfigs()
    {
        return _color_configs.all_color_configs;
    }

    public List<AudioConfig> GetAudioConfigs() 
    {
        return _audio_configs._all_audio_configs;
    }

    public List<StageConfig> GetStageConfigs()
    {
        return _stage_configs.all_stage_configs;
    }

    public StageConfig GetStageConfigById(int id)
    {
        foreach(StageConfig config in _stage_configs.all_stage_configs)
        {
            if(config.stage_id == id)
            {
                return config;
            }
        }
        return null;
    }

    public bool SetStageConfig()
    {
        current_stage_config = GetStageConfigById(current_stage);
        return current_stage_config != null ? true : false;
    }
}
