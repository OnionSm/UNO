using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, IDrawable
{
    public List<Transform> _list_card_in_hand { get; set; }
    [SerializeField] private GameController _game_controller;
    public void Draw(int amount)
    {
        List<Transform> list_card_got = _game_controller?.GetCard(amount); 
        if (list_card_got != null)
        {
            list_card_got.ForEach(card =>
            {
                _list_card_in_hand?.Add(card);  
            });
        }
        else
        {
            Debug.LogError("list_card_got is null");
        }
    }
}
