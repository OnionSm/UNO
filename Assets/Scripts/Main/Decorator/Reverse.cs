using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : BaseCard
{
    public override bool CanPlay()
    {
        throw new System.NotImplementedException();
    }

    public override void Play()
    {
        Controller.TurnDirection *= -1;
    }
}
