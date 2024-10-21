using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDrawDecision : IFSMDecision
{
    [Header("Game Controller")]
    [SerializeField] private GameController _controller;

    [Header("Enemy Core")]
    [SerializeField] private EnemyCore _enemy_core;
    public override bool Decision()
    {
        if(_controller._can_execute_after_draw)
        {
            // Get latest card in hand
            Transform card = _enemy_core._list_card_in_hand[_enemy_core._list_card_in_hand.Count - 1];

            CardType card_type = _controller.CurrentCardType;
            CardSymbol card_symbol = _controller.CurrentCardSymbol;
            BaseCard base_card = card.GetComponent<BaseCard>();
            if (base_card.Symbol == card_symbol || base_card.Type == card_type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
}
