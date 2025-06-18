using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteAction : IFSMAction
{
    [Header("Game Controller")]
    [SerializeField] private GameController _controller;

    [Header("Enemy Core")]
    [SerializeField] private EnemyCore _enemy_core;
    [SerializeField] private EnemyUI _enemy_ui;

    public override void Action()
    {
        CardType card_type = _controller.CurrentCardType;
        CardSymbol card_symbol = _controller.CurrentCardSymbol;
        CardColor card_color = _controller.CurrentColor;
        List<Transform> list_card = new List<Transform>();

        //if (_enemy_core._has_drawn_card_this_turn)
        //{
        //    list_card.Add(_enemy_core._list_card_in_hand[_enemy_core._list_card_in_hand.Count -1]);
        //}
        //else
        //{
        //    list_card = _enemy_core._list_card_in_hand;   
        //}

        //_enemy_core._has_drawn_card_this_turn = false;


        list_card = _enemy_core._list_card_in_hand;

        List<Transform> list_card_selection = CanPlayThisCard(list_card,card_color, card_symbol);
        Debug.Log($"Len List Card Selection Player {_enemy_core.turn_id}: {list_card_selection.Count}");
        if (list_card_selection == null)
            return;

        // Get the list of available card and random a card to play
        int randomIndex = Random.Range(0, list_card_selection.Count);
        list_card_selection[randomIndex].gameObject.GetComponent<BaseCard>().Play();
        _enemy_core._list_card_in_hand.Remove(list_card_selection[randomIndex]);
        _enemy_ui.SetCardLeftText(_enemy_core._list_card_in_hand.Count);
        
    }

    private List<Transform> CanPlayThisCard(List<Transform> list_card, CardColor card_color, CardSymbol card_symbol)
    {
        List<Transform> list_card_can_choose = new List<Transform>();
        foreach(Transform card in list_card)
        {
            BaseCard base_card = card.gameObject.GetComponent<BaseCard>();
            if(base_card == null)
            {
                return null;
            }
            
            
            if(base_card.Color == card_color || base_card.Symbol == card_symbol)
            {
                list_card_can_choose.Add(card);
            }
            
        }
        return list_card_can_choose;
    }
    
}
