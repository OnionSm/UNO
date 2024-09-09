using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DeckConfigs", menuName ="Deck/DeckConfigs")]
public class DeckConfigs : ScriptableObject
{
    public List<CardDeck> _deck_configs;
}

[System.Serializable]
public class CardDeck
{
    [Header("Card ID")]
    string card_id;
    [Header("Card Amount")]
    int amount;
}
