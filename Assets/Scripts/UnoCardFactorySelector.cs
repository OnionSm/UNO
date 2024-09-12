using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnoCardFactorySelector 
{
    private static readonly Dictionary<CardType, ICardFactory> factory_map = new Dictionary<CardType, ICardFactory>
    {
        { CardType.Skip, new SkipCardFactory() },
        { CardType.Reverse, new ReverseCardFactory() },
        { CardType.DrawTwo, new DrawCardFactory() },
        { CardType.Wild, new WildCardFactory() },
        { CardType.WildDrawFour, new DrawFourCardFactory() }
    };

    public static ICardFactory GetFactory(CardType card_type)
    {
        if (card_type >= CardType.Zero && card_type <= CardType.Nine)
        {
            return new NumberCardFactory();
        }

        return factory_map[card_type];
    }
}
