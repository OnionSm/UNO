using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int _turn_direction;
    [SerializeField] private List<int> _enemy;
    private int _player_count;
    private int _current_turn;
    private List<ICard> _deck;
    private List<ICard> _list_card_played;
    [SerializeField] private GameObject _card_holder;
    private ICard _current_card;
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    public void ChangeTurn(int value)
    {
        _current_turn += (value * 2);
        _current_turn %= _player_count;
    }
    public void EndMatch()
    {

    }
    public void ReverseTurn()
    {
        _turn_direction *= -1;
    }
    public void DrawCard(int value)
    {
        ChangeTurn(1);
        // người chơi tiếp theo draw value card
        ChangeTurn(1);
    }
    public void PlayCard(ICard card)
    {
        _list_card_played.Add(card);
        
    }
    public ICard GetLatestCard()
    {
        return _current_card;
    }
}
