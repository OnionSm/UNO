using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="CardConfigs", menuName = "Cards/CardConfigs")]
public class CardConfigs : MonoBehaviour
{
   public List<CardConfig> configs;
}
[System.Serializable]
public class CardConfig
{
    public string card_id;
    public CardColor card_color;
    public CardType card_type;
    public Sprite card_image;
    
}

