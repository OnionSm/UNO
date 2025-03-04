using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAction : IFSMAction
{
    [Header("Game Controller")]
    [SerializeField] private GameController _controller;

    [Header("Enemy Core")]
    [SerializeField] private EnemyCore _enemy_core;
    public override void Action()
    {
        if (_controller._card_drawn_amount > 0)
        {
            _enemy_core.Draw(_controller._card_drawn_amount);
            _enemy_core._has_drawn_card_by_effect = true;
            _controller._card_drawn_amount = 0;
        }
        else
        {
            _enemy_core.Draw(1);
        }
    }

   
}
