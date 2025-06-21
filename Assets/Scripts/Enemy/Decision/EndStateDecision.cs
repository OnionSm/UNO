using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStateDecision : IFSMDecision
{
    public override bool Decision()
    {
        //Debug.Log("Execute End State Decision");
        return true;
    }

    
}
