using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCore : MonoBehaviour, IDrawable
{
    [SerializeField] private EnemyUI _enemy_ui;
    [SerializeField] private RectTransform _card_pos;
    [SerializeField] private GameController _game_controller;
    public RectTransform Card_Pos
    {
        get { return _card_pos; }
        set { _card_pos = value; }
    }
    public List<Transform> _list_card_in_hand { get; set; }
    public List<FSMState> _list_states = new List<FSMState>();
    private void Awake()      
    {
        _list_card_in_hand = new List<Transform>();
    }
    void Start()
    {
        CheckCardsInHand();
    }

    void CheckCardsInHand()     
    {
        if (_list_card_in_hand.Count < 7)
        {
            Invoke(nameof(CheckCardsInHand), 0.25f);  
            return; 
        }
        _enemy_ui.SetCardLeftText(_list_card_in_hand.Count);
        Debug.Log($"Set up card amount text {_list_card_in_hand.Count} ");
    }

    public void Draw(int amount)
    {
        List<Transform> list_card_got = _game_controller?.GetCard(amount);  
        if (list_card_got != null)
        {
            list_card_got.ForEach(card =>
            {
                _list_card_in_hand?.Add(card);
                card.SetParent(_card_pos, false);
            });
        }
        else
        {
            Debug.LogError("list_card_got is null");
        }
        //_enemy_ui.SetCardLeftText(_list_card_in_hand.Count);
    }
}
