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
    public string card_id;
    [Header("Card Amount")]
    public int amount;

    public CardDeck Clone()
    {
        return new CardDeck
        {
            card_id = this.card_id,
            amount = this.amount
        };
    }
}
