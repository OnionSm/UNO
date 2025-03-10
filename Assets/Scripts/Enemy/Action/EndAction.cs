using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EndAction : IFSMAction
{
    [Header("Game Controller")]
    [SerializeField] private GameController _controller;

    [Header("Enemy Core")]
    [SerializeField] private EnemyCore _enemy_core;
    public override void Action()
    {
        _enemy_core._has_drawn_card_by_effect = true;
        _controller.ChangeTurn(_controller._turn_change);
        _controller._turn_change = 1;
    }

    
}
