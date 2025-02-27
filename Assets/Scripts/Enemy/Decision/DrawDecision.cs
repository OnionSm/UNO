using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDecision : IFSMDecision
{
    [Header("Game Controller")]
    [SerializeField] private GameController _controller;

    [Header("Enemy Core")]
    [SerializeField] private EnemyCore _enemy_core;
    public override bool Decision()
    {
        CardType card_type = _controller.CurrentCardType;
        CardSymbol card_symbol = _controller.CurrentCardSymbol;
        CardColor card_color = _controller.CurrentColor;
        if (card_type == CardType.Number)
        {
            List<Transform> _list_card = new List<Transform>();
            _list_card = _enemy_core._list_card_in_hand;
            if (_list_card.Count <= 0)
            {
                return false;
            }
            foreach (Transform card in _list_card)
            {
                BaseCard base_card = card.GetComponent<BaseCard>();
                if (base_card.Symbol == card_symbol || base_card.Color == card_color)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            if(_controller._card_drawn_amount <= 0)
            {
                return false;
            }
            return true;    
        }
    }

    
}
