using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardFactory : ICardFactory
{
    public BaseCard CreateCard()
    {
        return new AddTwo();
    }
}
