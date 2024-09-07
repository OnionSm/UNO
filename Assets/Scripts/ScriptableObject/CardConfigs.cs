using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="CardConfigs", menuName = "Cards/CardConfigs")]
public class CardConfigs : MonoBehaviour
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
    public Sprite card_image;
    
}

