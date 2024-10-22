using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard : MonoBehaviour 
{
    public string card_id {  get; set; }
    public CardColor Color { get; set; }
    public CardType Type { get; set; }
    public CardSymbol Symbol { get; set; }
    public GameController Controller { get; set; }
    public abstract void Play();
    public abstract bool CanPlay();
}
