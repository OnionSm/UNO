using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FSMState 
{
    public string _state_name;
    public List<IFSMAction> _list_actions = new List<IFSMAction>();
    public List<FSMTransition> _list_transitions = new List<FSMTransition>();

    public void UpdateState(EnemyCore core)
    {
        ExecuteAction(core);
        ExecuteTransition(core);
    }

    public void ExecuteAction(EnemyCore core)
    {

    }

    public void ExecuteTransition(EnemyCore core)
    {

    }
}
