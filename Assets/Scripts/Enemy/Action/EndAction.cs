using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EndAction : IFSMAction
{
    [Header("Game Controller")]
    [SerializeField] private GameController _controller;
    public override void Action()
    {
        _controller._can_execute_after_draw = true;
        _controller.ChangeTurn(1);
    }

    
}
