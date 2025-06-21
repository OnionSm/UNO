using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteAction : IFSMAction
{
    [Header("Game Controller")]
    [SerializeField] private GameController _controller;

    [Header("Enemy Core")]
    [SerializeField] private EnemyCore _enemy_core;
    [SerializeField] private EnemyUI _enemy_ui;

    public override void Action()
    {
        Debug.Log("Execute Action");
        CardType card_type = _controller.CurrentCardType;
        CardSymbol card_symbol = _controller.CurrentCardSymbol;
        CardColor card_color = _controller.CurrentColor;

        int amount_card_can_play = _enemy_core._list_card_can_play.Count;
        if (amount_card_can_play <= 0)
        {
            return;
        }
        int randomIndex = Random.Range(0, amount_card_can_play);
        Transform play_card = _enemy_core._list_card_can_play[randomIndex];
        play_card.gameObject.GetComponent<BaseCard>().Play();

        _enemy_core._list_card_in_hand.Remove(play_card);
        _enemy_ui.SetCardLeftText(_enemy_core._list_card_in_hand.Count);
    }
}
