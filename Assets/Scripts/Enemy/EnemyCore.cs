﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCore : MonoBehaviour, IDrawable, IObserver, ITurn
{
    [SerializeField] private EnemyUI _enemy_ui;
    [SerializeField] private RectTransform _card_pos;
    [SerializeField] private GameController _game_controller;


    private List<GameObject> list_player;
    private List<CardDeck> _deck_card;
    

    public RectTransform Card_Pos
    {
        get { return _card_pos; }
        set { _card_pos = value; }
    }
    public List<Transform> _list_card_in_hand { get; set; }

    public int turn_id { get ; set; }

    public List<FSMState> _list_states = new List<FSMState>();

    private string _current_state_name = "Waiting";
    private FSMState _current_state;

    public bool _has_drawn_card_by_effect { get; set; } = false;
    private void Awake()      
    {
        _list_card_in_hand = new List<Transform>();
    }

    void Start()
    {
        LoadComponent();
        LoadInitState();
        CheckCardsInHand();
    }

    void Update()
    {
        ExecuteState();
    }
    void LoadComponent()
    {
        this.list_player = new List<GameObject>();
        this._deck_card = new List<CardDeck>();
        this._deck_card = GameManager.Instance.GetDecks();
    }

    void LoadInitState()
    {
        _current_state = GetStateByName(_current_state_name);
    }

    void CheckCardsInHand()     
    {
        if (_list_card_in_hand.Count < 7)
        {
            Invoke(nameof(CheckCardsInHand), 0.25f);  
            return; 
        }
        _enemy_ui.SetCardLeftText(_list_card_in_hand.Count);
        //Debug.Log($"Set up card amount text {_list_card_in_hand.Count} ");
    }

    public void Draw(int amount)
    {
        List<Transform> list_card_got = _game_controller?.GetCard(amount);  
        if (list_card_got != null)
        {
            list_card_got.ForEach(card =>
            {
                _list_card_in_hand?.Add(card);
                RectTransform card_rect = card.gameObject.GetComponent<RectTransform>();
                _game_controller.SetPositionForCard(card_rect, _card_pos);
            });
        }
        else
        {
            Debug.LogError("list_card_got is null");
        }
        //_enemy_ui.SetCardLeftText(_list_card_in_hand.Count);
    }


    void GetCardAmountInEachPlayer()
    {
        List<GameObject> list_player_temp = new List<GameObject>();
        list_player_temp = _game_controller.ListPlayer;
        List<int> list_card_amount  = new List<int>();

        if (list_player == null || list_player.Count <= 0)
            return;

        foreach (GameObject player in list_player)
        {
            EnemyCore core = player.GetComponent<EnemyCore>();
            if (core != null)
                return;
            int amount = core._list_card_in_hand.Count;
            list_card_amount.Add(amount);
        }
        list_player.Clear();
        list_player = list_player_temp;
    }
    void ReCalculateCardDeck()
    {
        GameObject latest_card = _game_controller.GetLatestCard();
        if (latest_card == null)
            return;

        BaseCard base_card = latest_card.GetComponent<BaseCard>();
        for(int i = 0; i < _deck_card.Count; i++)
        {
            if (_deck_card[i].card_id == base_card.card_id)
            {
                _deck_card[i].amount = _deck_card[i].amount > 0 ? _deck_card[i].amount -- : 0;
            }
        }
    }
    public void Notify()
    {
        // Get card amount in each player hand
        GetCardAmountInEachPlayer();
        // Calculate the left amount of each card symbol
        ReCalculateCardDeck();
    }

    private FSMState GetStateByName(string state_name)
    {
        foreach(FSMState state in _list_states)
        {
            if(state._state_name == state_name)
            {
                return state;
            }
        }
        return null;
    }
    public void ChangeState(string state_name)
    {
        FSMState state = GetStateByName(state_name);
        if (state != null)
        {
            _current_state = state;
        }
    }
    private void ExecuteState()
    {
        if(CanExecuteState())
        {
            _current_state.ExecuteAction(this);
        }
    }
    private bool CanExecuteState()
    {
        return _list_card_in_hand.Count > 0;
    }
    
}
// Nếu trên tay có nhiều hơn 1 lá bài có thể đánh được thì sẽ ưu tiên 
// chọn lá bài số để đánh vì xác suất gặp số là 1/10 còn gặp màu là 1/4

// Nếu đối phương bốc bài thì có nghĩa là có 70% đối phương không có lá để đánh,
// Nếu đối phương bốc bài và không đánh thì có 90% là lá đó không đánh được và 10