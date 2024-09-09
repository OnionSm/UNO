using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCard : BaseCard
{
    private CardNumber _card_number;
    public CardNumber cardNumber
    {  
        get { return _card_number; } 
        set { _card_number = value; }
    }


    public override void Play()
    {
        
    }

    
}
