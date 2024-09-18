﻿using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;
using System.Linq;

public class GameController : MonoBehaviour
{
    [Header("Game Controller UI Manager")]
    [SerializeField] private GameControllerUIManager _game_controller_ui_manager;

    [Header("Card Dealer")]
    [SerializeField] private CardDealer _card_dealer;
    public CardColor CurrentColor { get; set; }
    public CardType CurrentType { get; set; }

    private int _turn_direction;
    public int TurnDirection
    {
        get { return _turn_direction; }
        set { _turn_direction = value; }
    }
    
    [SerializeField] private List<GameObject> _list_player;
    [SerializeField] private GameObject _card_holder;

    [SerializeField] private List<Transform> _deck;
    private List<GameObject> _list_card_played;

    private int _player_count;
    private int _current_turn;
    private GameObject _current_card;

    private ICardFactory _cardFactory;

    [SerializeField] private List<CardConfig> _list_card_configs = new List<CardConfig>();
    [SerializeField] private List<CardDeck> _deck_config = new List<CardDeck>();

    private void Start()
    {
        this._list_card_configs  = GameManager.Instance.GetListCardConfigs();
        this._deck_config = GameManager.Instance.GetDecks();
        InitCardDeck();
    }
    private void Update()
    {
        
    }
    
    private void InitCardDeck()
    {
        foreach(CardDeck card in _deck_config) 
        {
            foreach (CardConfig card_config in _list_card_configs)
            {
                if(card_config.card_id == card.card_id)
                {
                    for(int i = 0; i < card.amount; i++)
                    {
                        Transform new_card = UnoCardFactorySelector.GetFactory(card_config.card_type).CreateCard();
                        new_card.gameObject.SetActive(true);
                        BaseCard basecard = new_card.GetComponent<BaseCard>();
                        basecard.Color = card_config.card_color;
                        basecard.Type = card_config.card_type;
                        _deck.Add(new_card);
                        //Debug.Log($"Generated {card_config.card_type}");
                    }
                }
            }
        }
        SuffleDeck();
        _game_controller_ui_manager.SetCardAmountText(_deck.Count);
        foreach (var obj in _deck)
        {
            // Lấy component BaseCard từ GameObject
            BaseCard baseCard = obj.GetComponent<BaseCard>();

            // Kiểm tra nếu BaseCard không null (đã tồn tại trên obj)
            if (baseCard != null)
            {
                Debug.Log(baseCard);
            }
            else
            {
                Debug.Log("No BaseCard component found on " + obj.name);
            }
        }

    }
    public void ChangeTurn(int value)
    {
        _current_turn = GetNextTurn(value);
    }
    public int GetNextTurn(int value)
    {
        int temp = _current_turn +  (value * 2 * _turn_direction);
        if (temp < 0)
        {
            temp += _player_count;
        }
        temp %= _player_count;
        return temp;
    }
    public GameObject GetNextTurnPlayer()
    {
        int next_turn = GetNextTurn(1);
        return _list_player[next_turn];
    }
    public void EndMatch()
    {

    }
    public void ReverseTurn()
    {
        _turn_direction *= -1;
    }
    public void DrawCard(int amount, int offset)
    {
        GameObject player = GetNextTurnPlayer();
        player.GetComponent<IDrawable>()?.Draw(amount);
    }
    public void PlayCard(GameObject card)
    {
        _list_card_played.Add(card);
        
    }
    public GameObject GetLatestCard()
    {
        return _current_card;
    }

    public void PlayNumberCard(GameObject card)
    {
        _list_card_played.Add(card);
        SetCurrentAttributes(card);
    }

    public void SetCurrentAttributes(GameObject card)
    {

        CurrentColor = card.GetComponent<BaseCard>().Color;
        CurrentType = card.GetComponent<BaseCard>().Type;
        
        
    }
    public void SuffleDeck()
    {
        _deck = _deck.OrderBy(go => Random.value).ToList();
    }
    
}
