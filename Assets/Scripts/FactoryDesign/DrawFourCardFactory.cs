using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFourCardFactory : ICardFactory
{
    public Transform CreateCard()
    {
        Transform new_card = CardSpawner.Instance.Spawn("Card");
        new_card.gameObject.AddComponent<WildDrawFour>();
        return new_card;
    }
}
