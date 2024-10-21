using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnoCardFactorySelector 
{
    private static readonly Dictionary<CardSymbol, ICardFactory> factory_map = new Dictionary<CardSymbol, ICardFactory>
    {
        { CardSymbol.Skip, new SkipCardFactory() },
        { CardSymbol.Reverse, new ReverseCardFactory() },
        { CardSymbol.DrawTwo, new DrawCardFactory() },
        { CardSymbol.Wild, new WildCardFactory() },
        { CardSymbol.WildDrawFour, new DrawFourCardFactory() }
    };

    public static ICardFactory GetFactory(CardSymbol card_type)
    {
        if (card_type >= CardSymbol.Zero && card_type <= CardSymbol.Nine)
        {
            return new NumberCardFactory();
        }

        return factory_map[card_type];
    }
}
