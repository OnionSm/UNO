using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IObserver
{
    void Notify(int turn);
}
