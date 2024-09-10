using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildCardFactory : ICardFactory
{
    public BaseCard CreateCard()
    {
        return new WildCard();
    }
}
