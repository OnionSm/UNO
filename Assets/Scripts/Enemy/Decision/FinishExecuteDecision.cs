using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishExecuteDecision : IFSMDecision
{
    public override bool Decision()
    {
        //Debug.Log("Finish Execute Decision");
        return true;
    }
}
