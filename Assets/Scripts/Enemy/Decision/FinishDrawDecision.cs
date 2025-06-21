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
        //Debug.Log("Finish Draw Decision");
        if (!_enemy_core._has_drawn_card_by_effect)
        {
            // Get latest card in hand
            Transform card = _enemy_core._list_card_in_hand[_enemy_core._list_card_in_hand.Count - 1];

            CardSymbol card_symbol = _controller.CurrentCardSymbol;
            CardColor card_color = _controller.CurrentColor;
            BaseCard base_card = card.GetComponent<BaseCard>();
            if (base_card.Symbol == card_symbol || base_card.Color == card_color)
            {
                // Change to Execute Phase
                return true;
            }
            else
            {
                // Change to End Phase
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
}
