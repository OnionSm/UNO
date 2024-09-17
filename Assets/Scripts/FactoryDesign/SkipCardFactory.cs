using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCardFactory : ICardFactory
{
    public Transform  CreateCard()
    {
        Transform new_card = CardSpawner.Instance.Spawn("Card");
        new_card.gameObject.AddComponent<TurnBan>();
        return new_card;
    }
}
