using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCardFactory : ICardFactory
{
    public BaseCard CreateCard()
    {
        return new TurnBan();
    }
}
