using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildDrawFour : BaseCard
{
    public override bool CanPlay()
    {
        return true;
    }

    public override void Play()
    {
        Controller._card_drawn_amount += 4;
        Controller.SetCardSprite(gameObject.transform);
    }
}
