using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class GameController : MonoBehaviour
{
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
    [SerializeField] private string _card_name_prefabs;

    private List<GameObject> _deck;
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
                        Transform new_card = CardSpawner.Instance.Spawn(_card_name_prefabs, card_config.card_color,
                          card_config.card_type, card_config.card_image);
                        if(new_card == null)
                        {
                            return;
                        }
                        BaseCard baseCard = UnoCardFactorySelector.GetFactory(card_config.card_type).CreateCard();
                        new_card.gameObject.AddComponent(baseCard.GetType());
                    }
                }
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
}
