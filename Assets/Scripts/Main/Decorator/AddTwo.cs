using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTwo : CardDecorator
{
    private GameController gameController;
    public AddTwo(ICard card, GameController gameController) : base(card)
    {
        this.gameController = gameController;
    }
    public override void Play()
    {
        base.Play();
        
    }
}
