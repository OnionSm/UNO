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
        Debug.Log("Execute End Action");
        _enemy_core.EndTurn();
    }
}
