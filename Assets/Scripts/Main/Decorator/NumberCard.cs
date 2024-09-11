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
        if (Color == Controller.CurrentColor || Type == Controller.CurrentType)
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
