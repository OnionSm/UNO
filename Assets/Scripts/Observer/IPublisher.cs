using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPublisher 
{
    void AddObserver();
    void RemoveObserver();
    void Notify(int turn);
}
