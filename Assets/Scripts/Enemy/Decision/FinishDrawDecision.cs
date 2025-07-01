using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FinishDrawDecision : IFSMDecision
{
    [Header("Game Controller")]
    [SerializeField] private GameController _controller;

    [Header("Enemy Core")]
    [SerializeField] private EnemyCore _enemy_core;
    public override bool Decision()
    {
        Debug.Log("Finish Draw Decision");
        if (!_enemy_core._has_drawn_card_by_effect)
        {

            int amount_draw_this_turn = _enemy_core._list_card_draw_this_turn.Count;
            if (amount_draw_this_turn == 0)
            {
                return true; // Change to End Phase
            }
            // Get latest card in hand
            Transform latest_card = _enemy_core._list_card_draw_this_turn[amount_draw_this_turn - 1];

            CardSymbol card_symbol = _controller.CurrentCardSymbol;
            CardColor card_color = _controller.CurrentColor;
            BaseCard base_card = latest_card.GetComponent<BaseCard>();
            if (base_card.Symbol == card_symbol || base_card.Color == card_color || base_card.Color == CardColor.Black)
            {
                _enemy_core._list_card_can_play.Add(latest_card);
                return false;  // Change to Execute Phase
            }
            else
            {
                _controller.turn_finished = true;
                return true;  // Change to End Phase
            }
        }
        else
        {
            _controller.turn_finished = true;
            return true;  // Change to End Phase
        }
    }
    
}
