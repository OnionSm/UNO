using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour, IDrawable
{
    public List<Transform> _list_card_in_hand { get; set; }

    public void Draw(int amount)
    {
        throw new System.NotImplementedException();
    }
}
