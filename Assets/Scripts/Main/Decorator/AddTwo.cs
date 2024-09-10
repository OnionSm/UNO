using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class AddTwo : BaseCard
{
    public override bool CanPlay()
    {
        GameObject latest_card = Controller.GetLatestCard();
        if(latest_card == null )
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
        Controller.SetCurrentAttributes(gameObject);
        Controller.DrawCard(2,1);
        Controller.ChangeTurn(2);
    }
    
}
