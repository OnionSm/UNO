using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard 
{
    public CardColor Color { get; set; }
    public CardType Type { get; set; }
    public abstract void Play();
}
