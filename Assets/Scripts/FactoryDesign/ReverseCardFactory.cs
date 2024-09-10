using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseCardFactory : ICardFactory
{
    public BaseCard CreateCard()
    {
        return new Reverse();
    }
}
