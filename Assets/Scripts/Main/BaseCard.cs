using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard : MonoBehaviour 
{
    public CardColor Color { get; set; }
    public CardType Type { get; set; }
    public GameController Controller { get; set; }
    public abstract void Play();
    public abstract bool CanPlay();
}
