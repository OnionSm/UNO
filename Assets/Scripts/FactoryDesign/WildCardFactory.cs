using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildCardFactory : ICardFactory
{
    public Transform CreateCard()
    {
        Transform new_card = CardSpawner.Instance.Spawn("Card");
        new_card.gameObject.AddComponent<WildCard>();
        return new_card;
    }
}
