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
        foreach(IFSMAction action in _list_actions)
        {
            action.Action();
        }
    }

    public void ExecuteTransition(EnemyCore core)
    {
        foreach(FSMTransition transition in  _list_transitions)
        {
            bool result = transition.decide.Decision();
            //if (!result)
            //{
            //    Debug.Log($"Result: {result}");
            //}
            //Debug.Log($"False State: {transition.false_state}");

            if (result && transition.true_state!= null && transition.true_state.Length > 0 )
            {
                core.ChangeState(transition.true_state);
                //Debug.Log("Change to true state");
            }
            else if (!result && transition.false_state != null && transition.false_state.Length > 0)
            {
                core.ChangeState(transition.false_state);
                //Debug.Log("Change to false state");
            }
        }
    }
}
