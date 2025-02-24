using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDrawable, ITurn
{
    public List<Transform> _list_card_in_hand { get; set; } = new List<Transform>();

    
    public int turn_id { get; set; } 

    [Header("Grid Layout Group")]
    [SerializeField] private GridLayoutGroup _player_hand_group_layout;

    [SerializeField] private GameController _game_controller;

    [Header("Event")]
    [SerializeField] private GameEvent _display_play_btn_ev_listener;

    public void Draw(int amount)
    {
        List<Transform> list_card_got = _game_controller?.GetCard(amount);
        if (list_card_got != null)
        {
            list_card_got.ForEach(card =>
            {
                BaseCard base_card = card.GetComponent<BaseCard>();
                Debug.Log($"{base_card.Color} {base_card.Type} {base_card.Symbol}");
                card.GetComponentInChildren<CardModel>()._player_id = 0;
                _list_card_in_hand?.Add(card);
                card.SetParent(_player_hand_group_layout.transform, false);
                card.GetComponentInChildren<CardModel>().StartFlipUp();
                Debug.Log(card);
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

    public void CheckAnyAvailbleCard(int current_turn)
    {
        Debug.Log("Check Available Card Called");
        if(current_turn != 0)
        {
            return;
        }
        if (CheckColor(_game_controller.CurrentColor))
        {
            _display_play_btn_ev_listener?.RaiseEvent();
            return;
        }
        if(CheckSymbol(_game_controller.CurrentCardSymbol))
        {
            _display_play_btn_ev_listener?.RaiseEvent();
        }

    }
    
    public bool CheckSymbol(CardSymbol symbol)
    {
        foreach (Transform card in _list_card_in_hand)
        {
            BaseCard base_card = card.GetComponent<BaseCard>();
            if (base_card.Symbol == CardSymbol.WildDrawFour || base_card.Symbol == symbol)
            {
                return true;
            }
        }
        return false;
    }
    
    public bool CheckColor(CardColor color)
    {
        foreach(Transform card in _list_card_in_hand)
        {
            BaseCard base_card = card.GetComponent<BaseCard>();
            if(base_card.Color == CardColor.Black || base_card.Color == color)
            {
                return true;
            }
        }
        return false;
    }


}
