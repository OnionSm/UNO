using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CardConfigs", menuName = "Cards/CardConfigs")]
public class CardConfigs : ScriptableObject
{
    [Header("List Config for Cards")]
    public List<CardConfig> configs;
    [Header("Card Protecter")]
    public Sprite _card_protecter;
}
[System.Serializable]
public class CardConfig
{
    public string card_id;
    public CardColor card_color;
    public CardType card_type;
    public CardSymbol card_symbol;
    public Sprite card_image;

    public CardConfig Clone()
    {
        return new CardConfig
        {
            card_id = this.card_id,
            card_color = this.card_color,
            card_type = this.card_type,
            card_symbol = this.card_symbol,
            card_image = this.card_image
        };
    }
}

