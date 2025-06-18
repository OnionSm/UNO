using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTurnDecision : IFSMDecision
{
    [Header("Game Controller")]
    [SerializeField] private GameController _controller;

    [Header("Enemy Core")]
    [SerializeField] private EnemyCore _enemy_core;
    public override bool Decision()
    {
        if (_controller.CurrentTurn != _enemy_core.turn_id)
        {
            return true;
        }
        //Debug.Log($"This is player {_enemy_core.turn_id}"); 
        return false;
    }

}
