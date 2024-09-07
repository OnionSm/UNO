using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CardDecorator : ICard
{

    protected ICard card;

    public CardDecorator(ICard card)
    {
        this.card = card;
    }

    public virtual void Play()
    {
        card.Play();  
    }
}
