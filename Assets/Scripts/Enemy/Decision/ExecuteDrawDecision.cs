using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteDrawDecision : IFSMDecision
{
    [Header("Game Controller")]
    [SerializeField] private GameController _controller;

    [Header("Enemy Core")]
    [SerializeField] private EnemyCore _enemy_core;
    public override bool Decision()
    {
        //Debug.Log("Execute Draw Decision");
        if (_controller._card_drawn_amount <= 0)
        {
            // Change to Draw Phase
            return false;
        }
        CardType card_type = _controller.CurrentCardType;
        CardSymbol card_symbol = _controller.CurrentCardSymbol;
        CardColor card_color = _controller.CurrentColor;
        List<Transform> _list_card = new List<Transform>();
        _list_card = _enemy_core._list_card_in_hand;

        bool has_valid_card = HasValidCard(_list_card, card_color, card_symbol);
        if(has_valid_card)
        {
            // Change to Execute Phase
            return true;
        }
        else
        {
            // Change to Draw Phase
            return false;
        }
    }

    bool HasValidCard(List<Transform> _list_card, CardColor card_color, CardSymbol card_symbol)
    {
        foreach (Transform card in _list_card)
        {
            BaseCard base_card = card.GetComponent<BaseCard>();
            if (base_card.Symbol == card_symbol || base_card.Color == card_color)
            {
                return true;
            }
        }
        return false;
    }


}
