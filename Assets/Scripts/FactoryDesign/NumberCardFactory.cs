using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCardFactory : ICardFactory
{
    public Transform CreateCard()
    {
        Transform new_card = CardSpawner.Instance.Spawn("Card");
        new_card.gameObject.AddComponent<NumberCard>();
        return new_card;
    }
}
