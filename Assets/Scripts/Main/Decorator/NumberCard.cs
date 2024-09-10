using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCard : BaseCard, INumber
{
    public CardNumber card_number { get ; set ; }

    public override bool CanPlay()
    {
        if(Controller.CurrentColor != Color || Controller.CurrentNumber != card_number)
        {
            return true;
        }
        return false;
    }

    public override void Play()
    {
        Controller.PlayNumberCard(gameObject);
    }

    
}
