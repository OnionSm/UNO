using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardFactory : ICardFactory
{
    public Transform CreateCard()
    {
        Transform new_card = CardSpawner.Instance.Spawn("Card");
        new_card.gameObject.AddComponent<AddTwo>();
        return new_card;
    }
}
