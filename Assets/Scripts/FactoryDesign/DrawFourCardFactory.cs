using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFourCardFactory : ICardFactory
{
    public BaseCard CreateCard()
    {
        return new WildDrawFour();
    }
}
