using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDrawable, ITurn
{
    public List<Transform> _list_card_in_hand { get; set; }

    public int turn_id { get; set; } 

    [Header("Grid Layout Group")]
    [SerializeField] private GridLayoutGroup _player_hand_group_layout;

    [SerializeField] private GameController _game_controller;
    public void Draw(int amount)
    {
        List<Transform> list_card_got = _game_controller?.GetCard(amount);
        if (list_card_got != null)
        {
            list_card_got.ForEach(card =>
            {
                card.GetComponentInChildren<CardModel>()._player_id = 0;
                _list_card_in_hand?.Add(card);
                card.SetParent(_player_hand_group_layout.transform, false);
                card.GetComponentInChildren<CardModel>().StartFlipUp();
            });
        }
        else
        {
            Debug.LogError("list_card_got is null");
        }
    }

    private void Start()
    {
        
    }

    
    public void Update() { }

    private void LoadComponent()
    {
        this.turn_id = 0;
    }

    public void ClickCard()
    {

    }

}
