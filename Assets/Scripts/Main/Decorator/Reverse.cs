using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : CardDecorator
{
    private GameController controller;
    public Reverse(ICard card, GameController controller) : base(card)
    {
        this.controller = controller;
    }
    public override void Play()
    {
        base.Play();
        controller.ReverseTurn();
    }

}
