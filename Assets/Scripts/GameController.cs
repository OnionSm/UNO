using System.Collections;
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

    [Header("Deck position")]
    [SerializeField] private RectTransform _deck_rect;

    [Header("UI Enemy Object")]
    [SerializeField] private List<EnemyUIAttributes> _list_enemy_ui;

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

    private int _player_count = 1;
    private int _current_turn;
    private GameObject _current_card;

    private ICardFactory _cardFactory;

    [SerializeField] private List<CardConfig> _list_card_configs = new List<CardConfig>();
    [SerializeField] private List<CardDeck> _deck_config = new List<CardDeck>();
    private Sprite _protecter;

    private void Start()
    {
        this._list_card_configs  = GameManager.Instance.GetListCardConfigs();
        this._deck_config = GameManager.Instance.GetDecks();
        this._protecter = GameManager.Instance.GetProtecter();
        InitCardDeck();
    }
    private void Update()
    {
        
    }
    private void InitPlayer()
    {
        for(int i = 0; i < _player_count; i++)
        {
            Transform new_player = EnemySpawner.Instance.Spawn("Enemy");
            EnemyUI enemyUI = new_player.gameObject.GetComponent<EnemyUI>();
            EnemyCore enemyCore = new_player.gameObject.GetComponent<EnemyCore>();
            enemyUI.Card_Text_Left = _list_enemy_ui[i]._card_amount;
            enemyUI.Cash_Text = _list_enemy_ui[i]._cash_amount;
            enemyCore.Card_Pos = _list_enemy_ui[i]._deck_pos;
            _list_player.Add(new_player.gameObject);
        }
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
                        RectTransform new_card_rect = new_card.GetComponent<RectTransform>();
                        SetPositionForCard(new_card_rect, _deck_rect);
                        new_card.gameObject.SetActive(true);

                        CardModel model = new_card.GetComponentInChildren<CardModel>();
                        model.card_image = card_config.card_image;
                        model.card_protecter = _protecter;
                        model.LoadComponent();

                        BaseCard basecard = new_card.GetComponent<BaseCard>();
                        basecard.Color = card_config.card_color;
                        basecard.Type = card_config.card_type;
                        _deck.Add(new_card);
                        
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
    public void SetPositionForCard(RectTransform card, RectTransform parent)
    {
        card.SetParent(parent, false);
        card.sizeDelta = parent.sizeDelta;

        card.anchoredPosition = Vector3.zero; 
    }
}
