using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTurnDecision : IFSMDecision
{
    [Header("Game Controller")]
    [SerializeField] private GameController _controller;

    [Header("Turn ID")]
    [SerializeField] private int _turn_id;
    public override bool Decision()
    {
        if (_controller.CurrentTurn != _turn_id)
        {
            return false;
        }
        Debug.Log($"This is player {_turn_id}"); 
        return true;
    }

}
