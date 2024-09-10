using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCardFactory : ICardFactory
{
    public BaseCard CreateCard()
    {
        return new NumberCard();
    }
}
