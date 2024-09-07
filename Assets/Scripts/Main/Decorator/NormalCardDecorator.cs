using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCardDecorator : CardDecorator
{
    public CardType Type { get; set; }

    private GameController _controller;
    public NormalCardDecorator(ICard card, GameController controller) : base(card)
    {
        this._controller = controller;
    }
    public override void Play()
    {
        base.Play();
    }
    public CardType GetCardType()
    {
        return Type;
    }

}
