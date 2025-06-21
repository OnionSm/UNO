using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandbyDecision : IFSMDecision

{
    public override bool Decision()
    {
        Debug.Log("Standby Decision");
        return true;
    }

    
}
