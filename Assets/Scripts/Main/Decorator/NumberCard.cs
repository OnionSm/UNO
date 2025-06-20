using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCard : BaseCard
{
    public override bool CanPlay()
    {
        GameObject latest_card = Controller.GetLatestCard();
        if (latest_card == null)
        {
            return true;
        }
        if (Color == Controller.CurrentColor || Type == Controller.CurrentCardType)
        {
            return true;
        }
        return false;
    }

    public override void Play()
    {
        Controller.PlayCard(gameObject);
    }

    
}
