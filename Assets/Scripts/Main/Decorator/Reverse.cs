using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : BaseCard
{
    public override bool CanPlay()
    {
        GameObject latest_card = Controller.GetLatestCard();
        if (latest_card == null)
        {
            return true;
        }
        if (Color == Controller.CurrentColor)
        {
            return true;
        }
        return false;
    }

    public override void Play()
    {
        Controller.TurnDirection *= -1;
        Controller.PlayCard(gameObject);
    }
}
