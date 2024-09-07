using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ICard
{
    [SerializeField] private GameController _gameController;

    [Header("Attribute")]
    private ICard card;
    private CardType cardType;
    private CardColor cardColor;

    public void Register(ICard card)
    { 
        this.card = card; 
    }
    public void ChooseCard()
    {
        
        
    }

    public void Play()
    {
        card.Play();
        
    }
}
