using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;
using System.Linq;

public class GameController : MonoBehaviour, IPublisher
{
    [Header("Game Event")]
    [SerializeField] private GameEvent _on_player_turn_changed_ev;
    ////[SerializeField] private IntEvent _change_turn_event;
    //[SerializeField] private BoolEvent _play_card_btn_appearance_ev;
    //[SerializeField] private BoolEvent _drop_card_btn_appearance_ev;
    //[SerializeField] private BoolEvent _available_play_card_btn_ev;

    [Header("Game Controller UI Manager")]
    [SerializeField] private GameControllerUIManager _game_controller_ui_manager;

    [Header("Card Dealer")]
    [SerializeField] private CardDealer _card_dealer;

    [Header("Deck position")]
    [SerializeField] private RectTransform _deck_rect;

    [Header("UI Enemy Object")]
    [SerializeField] private List<EnemyUIAttributes> _list_enemy_ui;
    [SerializeField] private List<GameObject> _list_enemy_ui_zone;

    [Header("Played Zone")]
    [SerializeField] private RectTransform _played_zone;

    [Header("Card Color Manager")]
    [SerializeField] private CardColorManager _card_color_manager;

    private List<IObserver> _list_observer;

    public CardColor CurrentColor { get; set; }
    public CardType CurrentCardType { get; set; }
    public CardSymbol CurrentCardSymbol { get; set; }

    public int _card_drawn_amount { get; set; }

    public bool _can_execute_after_draw { get; set; }

    public int _turn_change {  get; set; }

    private List<ColorConfig> _list_color_config = new List<ColorConfig>();
    private int _turn_direction;
    public int TurnDirection
    {
        get { return _turn_direction; }
        set { _turn_direction = value; }
    }
    
    [SerializeField] private List<GameObject> _list_player;
    public List<GameObject> ListPlayer
    {
        get { return _list_player; }
        set { _list_player = value; }
    }
    [SerializeField] private GameObject _card_holder;

    [SerializeField] private List<Transform> _deck;
    private List<GameObject> _list_card_played;

    private int _player_count = 3;
    private int _current_turn;
    public int CurrentTurn
    {
        get { return _current_turn; }
        set { _current_turn = value; }
    }

    private GameObject _current_card;

    private ICardFactory _cardFactory;

    [SerializeField] private List<CardConfig> _list_card_configs = new List<CardConfig>();
    [SerializeField] private List<CardDeck> _deck_config = new List<CardDeck>();
    private Sprite _protecter;

    private bool _game_started = false;
    public bool GameStarted
    {
        get { return _game_started; }
        set { _game_started = value; }
    }


    private void Start()
    {
        InitPlayer();
        this._card_drawn_amount = 0;
        this._can_execute_after_draw = true;
        this._turn_change = 1;
        this._list_observer = new List<IObserver>();
        this._list_color_config = GameManager.Instance.GetColorConfigs();
        this._list_card_configs  = GameManager.Instance.GetListCardConfigs();
        this._deck_config = GameManager.Instance.GetDecks();
        this._protecter = GameManager.Instance.GetProtecter();
        InitCardDeck();
        _card_dealer.DistributeCard(_list_player, _deck);
        if(_current_turn == 0)
        {
            _on_player_turn_changed_ev?.RaiseEvent();
            CheckAvailableCard();
        }
        InitFirstCard();
        _game_controller_ui_manager.SetCardAmountText(_deck.Count);
        _on_player_turn_changed_ev?.RaiseEvent();
    }
    private void Update()
    {
        
    }
    private void InitPlayer()
    {
        for(int i = 0; i < _player_count; i++)
        {
            Transform new_player = EnemySpawner.Instance.Spawn("Enemy");
            IObserver observer  = new_player.gameObject.GetComponent<IObserver>();
            //_list_observer.Add(observer);
            EnemyUI enemyUI = new_player.gameObject.GetComponent<EnemyUI>();
            EnemyCore enemyCore = new_player.gameObject.GetComponent<EnemyCore>();
            ITurn turnComponent = new_player.gameObject.GetComponent<ITurn>();
            if (turnComponent != null)
            {
                turnComponent.turn_id = i+1;
            }
            enemyUI.Card_Text_Left = _list_enemy_ui[i]._card_amount;
            enemyUI.Cash_Text = _list_enemy_ui[i]._cash_amount;
            enemyCore.Card_Pos = _list_enemy_ui[i]._deck_pos;
            _list_enemy_ui_zone[i].gameObject.SetActive(true);
            _list_player.Add(new_player.gameObject);
        }
    }
    private void InitFirstCard()
    {
        Transform card = _deck[0];
        BaseCard base_card = card.GetComponent<BaseCard>();
        if(base_card.Color == CardColor.Black)
        {
            int color_index = Random.Range(0, _list_color_config.Count);
            CardColor  selected_color = _list_color_config[color_index].type_color;
        }
        //CurrentColor = base_card.Color;
        //CurrentCardSymbol = base_card.Symbol;
        //CurrentCardType = base_card.Type;
        SetCurrentAttributes(card.gameObject);
        _deck.Remove(card);
        RectTransform card_rect = card.gameObject.GetComponent<RectTransform>();
        SetPositionForCard(card_rect, _played_zone);
        card.GetComponentInChildren<CardModel>().StartFlipUp();
        Debug.Log($"Current card {CurrentColor} {CurrentCardSymbol} {CurrentCardType}");
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
                        Transform new_card = UnoCardFactorySelector.GetFactory(card_config.card_symbol).CreateCard();
                        RectTransform new_card_rect = new_card.GetComponent<RectTransform>();
                        SetPositionForCard(new_card_rect, _deck_rect);
                        new_card.gameObject.SetActive(true);

                        CardModel model = new_card.GetComponentInChildren<CardModel>();
                        model.card_image = card_config.card_image;
                        model.card_protecter = _protecter;
                        model.LoadComponent();

                        BaseCard basecard = new_card.GetComponent<BaseCard>();
                        basecard.card_id = card.card_id;
                        basecard.Color = card_config.card_color;
                        basecard.Symbol = card_config.card_symbol;
                        basecard.Type = card_config.card_type;
                        _deck.Add(new_card);
                    }
                }
            }
        }
        SuffleDeck();
        _game_controller_ui_manager.SetCardAmountText(_deck.Count);

        // This code is used for check all cards which generated
        foreach (var obj in _deck)
        {
            // Lấy component BaseCard từ GameObject
            BaseCard baseCard = obj.GetComponent<BaseCard>();

            // Kiểm tra nếu BaseCard không null (đã tồn tại trên obj)
            if (baseCard != null)
            {
                //Debug.Log(baseCard);
            }
            else
            {
                Debug.Log("No BaseCard component found on " + obj.name);
            }
        }

    }

    // Change turn to current turn + value
    public void ChangeTurn(int value)
    {
        _current_turn = GetNextTurn(value);
        //_change_turn_event?.RaiseEvent(_current_turn);
        if (_current_turn == 0)
        {
            _on_player_turn_changed_ev?.RaiseEvent();
        }
    }

    // Get the index player that they has turn
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
    public List<Transform> GetCard(int amount)
    {
        List<Transform> list_cards_player_get = new List<Transform>();
        for(int  i = 0; i < amount; i++)
        {
            Transform card_get = _deck[0];
            if (card_get == null)
            {
                return null;
            }
            list_cards_player_get.Add(card_get);
            _deck.Remove(card_get);
            //Debug.Log(_deck.Count);
        }
        return list_cards_player_get;
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

        SetCurrentColorAttributes(card.GetComponent<BaseCard>().Color);
        CurrentCardType = card.GetComponent<BaseCard>().Type;
        CurrentCardSymbol = card.GetComponent<BaseCard>().Symbol;
    }

    public void SetCurrentColorAttributes(CardColor card_color)
    {
        CurrentColor = card_color;
        _card_color_manager.UpdateColorPanel(card_color);
    }
    public void SuffleDeck()
    {
        _deck = _deck.OrderBy(go => Random.value).ToList();
    }
    public void SetPositionForCard(RectTransform card, RectTransform parent)
    {
        card.SetParent(parent.transform, false);
        card.sizeDelta = parent.sizeDelta;
        card.anchoredPosition = Vector2.zero;
        card.anchorMin = parent.anchorMin;
        card.anchorMax = parent.anchorMax;
        card.pivot = parent.pivot;
        card.localRotation = parent.localRotation;
        card.localScale = parent.localScale;
        card.localPosition = parent.localPosition;
    }

    public void AddObserver()
    {
        throw new System.NotImplementedException();
    }

    public void RemoveObserver()
    {
        throw new System.NotImplementedException();
    }

    public void Notify()
    {
        
    }
    
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
    }

    private void CheckAvailableCard()
    {

    }
    
}
