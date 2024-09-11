using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : Spawner
{
    public static CardSpawner Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("More than one Card Spawner");
        }
    }
}
