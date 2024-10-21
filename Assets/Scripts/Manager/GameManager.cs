using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardConfigs _card_configs;
    [SerializeField] private DeckConfigs _deck_configs;
    [SerializeField] private ColorConfigs _color_configs;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("More than one Game Manager");
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
}
