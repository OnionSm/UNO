using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrawable 
{
    public List<Transform> _list_card_in_hand { get; set; }
    void Draw(int amount);
}
