﻿using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDrawable, ITurn, IObserver
{
    public List<Transform> _list_card_in_hand { get; set; } = new List<Transform>();

    
    [SerializeField] public int turn_id { get; set; }
    [SerializeField] private bool _has_drawn = false;
    [SerializeField] private bool _can_draw = true;

    [Header("Grid Layout Group")]
    [SerializeField] private GridLayoutGroup _player_hand_group_layout;

    [SerializeField] private GameController _game_controller;

    [Header("Event")]
    [SerializeField] private BoolEvent _on_play_card_appear_btn_ev;
    [SerializeField] private BoolEvent _on_drop_turn_appear_btn_ev;
    [SerializeField] private BoolEvent _on_available_play_card_btn_ev;

    [Header("Card Selected")]
    [SerializeField] private GameObject _current_card_selected;



    public void Draw(int amount)
    {
        if(_game_controller._current_turn != 0 || _has_drawn || !_can_draw)
        {
            return;
        }
        List<Transform> list_card_got = _game_controller?.GetCard(amount);
        if (list_card_got != null)
        {
            list_card_got.ForEach(card =>
            {
                BaseCard base_card = card.GetComponent<BaseCard>();
                //Debug.Log($"{base_card.Color} {base_card.Type} {base_card.Symbol}");
                card.GetComponent<CardController>()._player_id = 0;
                _list_card_in_hand?.Add(card);
                card.SetParent(_player_hand_group_layout.transform, false);
                card.GetComponentInChildren<CardModel>().StartFlipUp();
                _has_drawn = true;
            });
        }
        else
        {
            Debug.LogError("list_card_got is null");
        }
    }
    
    private void Start()
    {
        LoadComponent();

    }
    

    private void LoadComponent()
    {
        this.turn_id = 0;
        this._has_drawn = false;
    }


    public void CheckAnyAvailbleCard()
    {
        //Debug.Log("Check Available Card Called----------------------------------------------");
        if (CheckColor(_game_controller.CurrentColor) || CheckSymbol(_game_controller.CurrentCardSymbol))
        {
            this._can_draw = false;
            Debug.Log("Has card to play");
            _on_play_card_appear_btn_ev?.RaiseEvent(true);
        }
        else
        {
            Debug.Log("Has not card to play");
            Debug.Log($"Has drawn: {_has_drawn}");
            if(this._has_drawn)
            {
                _game_controller.turn_finished = true;
                EndTurn();
            }
            else
            {
                this._can_draw = true;
            }
                

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
    public void CheckSeparateCard(GameObject card, CardColor color, CardType type, CardSymbol symbol)
    {
        ChangeCurrentSelectedCard();
        _current_card_selected = card;
        if (color == CardColor.Black || color == _game_controller.CurrentColor || _game_controller.CurrentCardSymbol == symbol)
        {
            //Debug.Log("Card Valid");
            _on_available_play_card_btn_ev?.RaiseEvent(true);
        }
        else
        {
            //Debug.Log("Card UnValid");
            _on_available_play_card_btn_ev?.RaiseEvent(false);
        }
    }
    private void ChangeCurrentSelectedCard()
    {
        if (_current_card_selected != null)
        {
            _current_card_selected.GetComponent<CardController>()?.OnCardSelection();
            UnSelectCard();
        }
    }
    public void UnSelectCard()
    {
        _current_card_selected = null;
    }

    public void PlayCard()
    {
        if (_current_card_selected != null)
        {
            _current_card_selected.GetComponent<BaseCard>()?.Play();
            _list_card_in_hand.Remove(_current_card_selected.transform);
            Debug.Log("Play card");

            EndTurn();
        }
        
    }
    public void EndTurn()
    {
        Winning();
        DisablePlayerUI();
        _current_card_selected = null;
        _game_controller.ChangeTurn();
        _has_drawn = false;
    }

    public void DisablePlayerUI()
    {
        _on_available_play_card_btn_ev?.RaiseEvent(false);
        _on_play_card_appear_btn_ev?.RaiseEvent(false);
    }

    public void Notify()
    {
        _has_drawn = false;
        int draw_amount = _game_controller._card_drawn_amount;
        if (draw_amount > 0)
        {
            Debug.Log("Player Draw");
            _can_draw = true;
            Draw(draw_amount);
            _game_controller._card_drawn_amount = 0;
            _game_controller.turn_finished = true;
            EndTurn();
            return;
        }
        Debug.Log($"Number Card In Player Hand: {_list_card_in_hand.Count}");
        _on_drop_turn_appear_btn_ev?.RaiseEvent(true);
        CheckAnyAvailbleCard();
    }

    public void Winning()
    {
        if (CheckWinCondition())
        {
            _game_controller.EndMatch(turn_id);
        }
    }

    public bool CheckWinCondition()
    {
        if (_list_card_in_hand.Count <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DespawnAllCard()
    {
        foreach (Transform card in _list_card_in_hand)
        {
            CardSpawner.Instance.Despawn(card);
        }
        _list_card_in_hand?.Clear();
    }
    public void ResetPlayer()
    {
        StopAllCoroutines();
        DespawnAllCard();
        _current_card_selected = null;

    }
}
