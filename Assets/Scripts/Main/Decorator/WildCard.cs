using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildCard : BaseCard
{
    public override bool CanPlay()
    {
        return true;
    }

    public override void Play()
    {
        // Change color
        Controller.SetCardSprite(gameObject.transform);
    }
}
