using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameController : MonoBehaviour
{
    private int _turn_direction;
    [SerializeField] private List<GameObject> _list_player;
    private int _player_count;
    private int _current_turn;
    private List<BaseCard> _deck;
    private List<BaseCard> _list_card_played;
    [SerializeField] private GameObject _card_holder;
    private BaseCard _current_card;
    private void Start()
    {
        
    }
    private void Update()
    {
        
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
    public void PlayCard(BaseCard card)
    {
        _list_card_played.Add(card);
        
    }
    /*public ICard GetLatestCard()
    {
        return _current_card;
    }*/

    public void PlayNumberCard(BaseCard card)
    {

    }
}
