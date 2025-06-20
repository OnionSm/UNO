using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
    [SerializeField] private Image _played_zone;

    [Header("Card Color Manager")]
    [SerializeField] private CardColorManager _card_color_manager;

    private List<IObserver> _list_observer = new List<IObserver>();

    #region Properties
    public CardColor CurrentColor { get; set; }
    public CardType CurrentCardType { get; set; }
    public CardSymbol CurrentCardSymbol { get; set; }

    public int _card_drawn_amount { get; set; }
        
    public bool _can_execute_after_draw { get; set; }

    public int turn_change {  get; set; }

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

    //[SerializeField] private List<Transform> _deck;
    private List<GameObject> _list_card_played = new List<GameObject>();

    private int _player_count = 3;
    private int _current_turn;
    public int CurrentTurn
    {
        get { return _current_turn; }
        set { _current_turn = value; }
    }

    private GameObject _current_card;

    private ICardFactory _cardFactory;
    #endregion

    #region CardConfig
    [SerializeField] private List<CardConfig> _list_card_configs = new List<CardConfig>();
    [SerializeField] private List<CardDeck> _deck_config = new List<CardDeck>();
    private List<ColorConfig> _list_color_config = new List<ColorConfig>();
    private Dictionary<string, CardConfig> _card_config_map = new Dictionary<string, CardConfig>();
    private Dictionary<string, CardDeck> _deck_config_map = new Dictionary<string, CardDeck>();
    private Sprite _protecter;
    #endregion

    private List<CardConfig> _card_in_deck_remain = new List<CardConfig>();

    private bool _game_started = false;
    public bool GameStarted
    {
        get { return _game_started; }
        set { _game_started = value; }
    }


    private void Start()
    {
        InitPlayer();
        LoadComponent();
        AddObserver();

        _card_dealer.DistributeCard(_list_player, _card_in_deck_remain);
        if(_current_turn == 0)
        {
            _on_player_turn_changed_ev?.RaiseEvent();
            CheckAvailableCard();
        }
        InitFirstCard();
        _game_controller_ui_manager.SetCardAmountText(_card_in_deck_remain.Count);
        _on_player_turn_changed_ev?.RaiseEvent();
    }
    private void Update()
    {
        
    }

    #region Load Component
    private void LoadComponent()
    {
        this._card_drawn_amount = 0;
        this._can_execute_after_draw = true;
        this.turn_change = 1;
        this._turn_direction = 1;
        this._list_observer = new List<IObserver>();
        this._list_color_config = GameManager.Instance.GetColorConfigs();
        this._list_card_configs = GameManager.Instance.GetListCardConfigs();
        this._deck_config = GameManager.Instance.GetDecks();
        this._protecter = GameManager.Instance.GetProtecter();

        LoadCardConfigMap();
        LoadDeckConfigMap();
        LoadDeck();
    }
    private void LoadCardConfigMap()
    {
        if (_list_card_configs == null)
        {
            return;
        }
        foreach (CardConfig config in _list_card_configs)
        {
            if (!_card_config_map.ContainsKey(config.card_id))
            {
                _card_config_map[config.card_id] = config;
            }
        }
    }
    private void LoadDeckConfigMap()
    {
        if (_deck_config == null)
        {
            return;
        }
        foreach (CardDeck cardDeck in _deck_config)
        {
            if (!_deck_config_map.ContainsKey(cardDeck.card_id))
            {
                _deck_config_map[cardDeck.card_id] = cardDeck;
            }
        }
    }
    private void LoadDeck()
    {
        foreach (CardDeck card in _deck_config)
        {
            int amount = card.amount;

            if (_card_config_map.TryGetValue(card.card_id, out CardConfig card_config))
            {
                List<CardConfig> list_insert = new List<CardConfig>(amount);
                for (int i = 0; i < amount; i++)
                {
                    list_insert.Add(card_config);
                }

                _card_in_deck_remain.AddRange(list_insert);
            }
        }
        SuffleDeck();
        //_game_controller_ui_manager.SetCardAmountText(_card_in_deck_remain.Count);
    }
    #endregion

    #region Spaawn Card
    private Transform SpawnCard(CardConfig card_config)
    {
        Transform new_card = UnoCardFactorySelector.GetFactory(card_config.card_symbol).CreateCard();
        //RectTransform new_card_rect = new_card.GetComponent<RectTransform>();
        //SetPositionForCard(new_card_rect, _deck_rect);
        new_card.gameObject.SetActive(true);

        CardModel model = new_card.GetComponentInChildren<CardModel>();
        model.card_image = card_config.card_image;
        //Debug.Log(card_config.card_image.ToString());
        model.card_protecter = _protecter;
        model.LoadComponent();

        BaseCard basecard = new_card.GetComponent<BaseCard>();
        basecard.card_id = card_config.card_id;
        basecard.Color = card_config.card_color;
        basecard.Symbol = card_config.card_symbol;
        basecard.Type = card_config.card_type;
        basecard.Controller = this;
        basecard.GameControllerUIManager = _game_controller_ui_manager;
        //_deck.Add(new_card);
        return new_card;
    }

    #endregion

    #region Init

    private void InitPlayer()
    {

        for (int i = 0; i < _player_count; i++)
        {
            Transform new_player = EnemySpawner.Instance.Spawn("Enemy");
            IObserver observer = new_player.gameObject.GetComponent<IObserver>();
            //_list_observer.Add(observer);
            EnemyUI enemyUI = new_player.gameObject.GetComponent<EnemyUI>();
            EnemyCore enemyCore = new_player.gameObject.GetComponent<EnemyCore>();
            ITurn turnComponent = new_player.gameObject.GetComponent<ITurn>();
            if (turnComponent != null)
            {
                turnComponent.turn_id = i + 1;
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
        Transform card = null;
        if (_card_in_deck_remain.Count <= 0)
        {
            return;
        }
        CardConfig card_config = _card_in_deck_remain[0];
        card = SpawnCard(card_config);
        CardColor new_color = card_config.card_color;
        if (card_config.card_color == CardColor.Black)
        {
            int color_index = Random.Range(0, _list_color_config.Count);
            new_color = _list_color_config[color_index].type_color;
        }
        SetCurrentAttributes(new_color, card_config.card_type, card_config.card_symbol);
        _card_in_deck_remain.Remove(card_config);
        SetCardSprite(card);
        //SetPositionForCard(card_rect, _played_zone);
        //card.GetComponentInChildren<CardModel>().StartFlipUp();
        //Debug.Log($"Current card {CurrentColor} {CurrentCardSymbol} {CurrentCardType}");
    }
    #endregion

    #region Turn

    // Change turn to current turn + value
    public void ChangeTurn()
    {
        _current_turn = GetNextTurn();
        //_change_turn_event?.RaiseEvent(_current_turn);
        //if (_current_turn == 0)
        //{
        //    //_on_player_turn_changed_ev?.RaiseEvent();
        //}
        Notify(_current_turn);

        Debug.Log($"Current Turn: {_current_turn}");
    }

    // Get the index player that they has turn
    public int GetNextTurn()
    {
        //Debug.Log($"Player count: {_player_count}");
        int temp = _current_turn +  (turn_change * _turn_direction);
        if (temp < 0)
        {
            temp += (_player_count + 1);
        }
        temp %= (_player_count + 1);
        return temp;
    }

    public void ReverseTurn()
    {
        _turn_direction *= -1;
    }

    #endregion

    #region Card

    public List<Transform> GetCard(int amount)
    {
        List<Transform> list_cards_player_get = new List<Transform>(amount);
        for(int i = 0; i < amount; i++)
        {
            CardConfig config = _card_in_deck_remain[0];
            if (config == null)
                return null;
            Transform new_card = SpawnCard(config);
            if (new_card == null)
                return null;
            list_cards_player_get.Add(new_card);
            _card_in_deck_remain.Remove(config);
        }
        _game_controller_ui_manager.SetCardAmountText(_card_in_deck_remain.Count());
        return list_cards_player_get;
    }

    public GameObject GetLatestCard()
    {
        return _current_card;
    }

    public void SuffleDeck()
    {
        _card_in_deck_remain = _card_in_deck_remain.OrderBy(go => Random.value).ToList();
    }
    #endregion

    #region PlayCard

    public void PlayCard(GameObject card)
    {
        
        var model = card.GetComponentInChildren<CardModel>();
        if (model == null) { Debug.Log("Model null"); return; }

        Sequence seq = model.PlayCardAnimation(_played_zone.GetComponent<RectTransform>());
        if (seq != null)
        {
            seq.OnComplete(() =>
            {
                SetCardSprite(card.transform);
                _list_card_played.Add(card);
                SetCurrentAttributes(card);

            });


        }
    }
    
    //public void PlayNumberCard(GameObject card)
    //{
    //    _list_card_played.Add(card);
    //    SetCurrentAttributes(card);
    //    Debug.Log($"Current card {CurrentColor} {CurrentCardSymbol} {CurrentCardType}");
    //}

    //public void PlayWildCard(CardColor color)
    //{
    //    if (_current_turn == 0)
    //    {
    //        _game_controller_ui_manager.OpenColorSelectionPanel();
    //    }
    //    else
    //    {
    //        CardColor new_color = GenerateColor();
    //        SetCurrentColorAttributes(new_color);
    //    }
    //}

    #endregion

    #region Set Attributes
    public void SetCurrentAttributes(GameObject card)
    {
        if (!card.TryGetComponent<BaseCard>(out var baseCard))
        {
            Debug.LogError($"BaseCard missing on {card.name}");
            return;
        }

        SetCurrentColorAttributes(baseCard.Color);
        CurrentCardType = baseCard.Type;
        CurrentCardSymbol = baseCard.Symbol;
    }
    public void SetCurrentAttributes(CardColor color, CardType type, CardSymbol symbol)
    {
        SetCurrentColorAttributes(color);
        CurrentCardType = type;
        CurrentCardSymbol = symbol;
    }
    public void SetCurrentColorAttributes(CardColor card_color)
    {
        if(card_color == CardColor.Black)
        {
            if (_current_turn == 0)
            {
                _game_controller_ui_manager.OpenColorSelectionPanel();
            }
            else
            {
                CardColor new_color = GenerateColor();
                SetCurrentColorAttributes(new_color);
            }
        }
        else
        {
            CurrentColor = card_color;
            _card_color_manager.UpdateColorPanel(card_color);
        }
    }
    public void ChangeColor(int colorIndex)
    {
        if (colorIndex < 0 || colorIndex > 3)
            return;
        SetCurrentColorAttributes((CardColor)colorIndex);
    }

    public void SetPositionForCard(RectTransform card, RectTransform parent)
    {
        // Bảo đảm thứ tự: gán cha trước, sau đó mới copy các thuộc tính
        card.SetParent(parent, false);          // worldPositionStays = false

        // Sao chép toàn bộ thông số RectTransform
        card.anchorMin = parent.anchorMin;
        card.anchorMax = parent.anchorMax;
        card.pivot = parent.pivot;
        card.sizeDelta = parent.sizeDelta;
        card.anchoredPosition = parent.anchoredPosition;   // Giữ nguyên offset nếu có
        card.localPosition = parent.localPosition;      // Trường hợp không dùng anchoring
        card.localRotation = parent.localRotation;
        card.localScale = parent.localScale;
    }

    public CardColor GenerateColor()
    {
        CardColor[] colors = { CardColor.Red, CardColor.Yellow, CardColor.Green, CardColor.Blue };
        CardColor randomColor = colors[Random.Range(0, colors.Length)];
        return randomColor;
    }
    public void SetCardSprite(Transform card)
    {
        var card_sprite = card.GetComponentInChildren<CardModel>()?.card_image;
        _played_zone.sprite = card_sprite;
        CardSpawner.Instance.Despawn(card);
        card.gameObject.SetActive(false);
    }

    #endregion 

    #region Observer
    public void AddObserver()
    {
        foreach(GameObject player in _list_player)
        {
            IObserver observer = player.GetComponent<IObserver>();
            _list_observer.Add(observer);
        }   
        Debug.Log($"Has {_list_observer.Count} Observer");
    }

    public void RemoveObserver()
    {
        throw new System.NotImplementedException();
    }

    public void Notify(int turn)
    {
        //Debug.Log($"Notify current turn: {CurrentTurn} and has {_list_observer.Count} Observer");
        _list_observer[CurrentTurn].Notify(turn);
    }
    #endregion

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
    }

    private void CheckAvailableCard()
    {

    }
   


    //public GameObject GetNextTurnPlayer()
    //{
    //    int next_turn = GetNextTurn(1);
    //    return _list_player[next_turn];
    //}
    public void EndMatch()
    {

    }

    //public void PlayCard(GameObject card)
    //{
    //    _list_card_played.Add(card);


    //}
}
