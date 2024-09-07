using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBan : CardDecorator
{

    private GameController _controller;
    public TurnBan(ICard card, GameController controller) : base(card)
    {
        _controller = controller;
    }
    public override void Play()
    {
        base.Play();
        _controller.PlayCard(this);
        _controller.ChangeTurn(2);
    }

}
