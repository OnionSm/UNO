using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour, IPublisher
{
    #region UI and Component
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

    [Header("Card Holder")]
    [SerializeField] private GameObject _card_holder;

    [Header("On Back Home Event")]
    [SerializeField] private GameEvent _on_back_home_game_event;
    #endregion

    private List<IObserver> _list_observer = new List<IObserver>();

    #region Properties
    private StageConfig _stage_config;
    public CardColor CurrentColor { get; set; }
    public CardType CurrentCardType { get; set; }
    public CardSymbol CurrentCardSymbol { get; set; }
    public int pre_player_id { get; set; }

    public int _card_drawn_amount { get; set; }

    public int turn_change {  get; set; }

    public int _turn_direction { get; set; }
  
    [SerializeField] private GameObject _main_player;
    
    [SerializeField] private List<GameObject> _list_player;

    private List<GameObject> _list_card_played = new List<GameObject>();

    private int _enemy_count;
    public int _current_turn { get; set; }
    public bool turn_finished; 

    #endregion

    #region CardConfig
    private List<CardConfig> _list_card_configs = new List<CardConfig>();
    private List<CardDeck> _deck_config = new List<CardDeck>();
    private List<ColorConfig> _list_color_config = new List<ColorConfig>();
    private Dictionary<string, CardConfig> _card_config_map = new Dictionary<string, CardConfig>();
    private Dictionary<string, CardDeck> _deck_config_map = new Dictionary<string, CardDeck>();
    private Sprite _protecter;
    #endregion

    #region Deck

    private List<CardConfig> _card_in_deck_remain = new List<CardConfig>();

    #endregion



    private void Start()
    {
        LoadComponent();
    }


    #region Load Component

    private void LoadStageConfig()
    {
        this._stage_config = GameManager.Instance.current_stage_config;
        this._enemy_count = _stage_config.num_player - 1;
    }
    private void LoadConfig()
    {
        _list_color_config = GameManager.Instance.GetColorConfigs();
        _list_card_configs = GameManager.Instance.GetListCardConfigs();
        _deck_config = GameManager.Instance.GetDecks();
        _protecter = GameManager.Instance.GetProtecter();

        LoadCardConfigMap();
        LoadDeckConfigMap();
        LoadDeck();
    }
    public void LoadBasicProperties()
    {
        pre_player_id = -1;
        _card_drawn_amount = 0;
        turn_change = 1;
        _turn_direction = 1;
        _current_turn = 0;
        turn_finished = false;
    }
    private void LoadComponent()
    {
        LoadStageConfig();
        InitPlayer();
        InitEnemy();

        LoadBasicProperties();
        LoadConfig();
        
        _game_controller_ui_manager.InitPanelUIState();

        AddObserver();

        _card_dealer.DistributeCard(_list_player, _card_in_deck_remain);
        InitFirstCard();
        _game_controller_ui_manager.SetCardAmountText(_card_in_deck_remain.Count);
        Notify();
        _game_controller_ui_manager.EnableLightBar(_current_turn);

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
        //Debug.Log($"Card in deck reamin: {_card_in_deck_remain.Count}");
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
        _list_player = new List<GameObject>();
        _list_player.Add(_main_player);
    }
    private void InitEnemy()
    {

        for (int i = 0; i < _enemy_count; i++)
        {
            Transform new_player = EnemySpawner.Instance.Spawn("Enemy");
            IObserver observer = new_player.gameObject.GetComponent<IObserver>();
            EnemyUI enemyUI = new_player.gameObject.GetComponent<EnemyUI>();
            EnemyCore enemyCore = new_player.gameObject.GetComponent<EnemyCore>();
            ITurn turnComponent = new_player.gameObject.GetComponent<ITurn>();
            if (turnComponent != null)
            {
                turnComponent.turn_id = i + 1;
                enemyCore.Turn_Id = turnComponent.turn_id;
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
        if(!turn_finished)
        {
            Invoke(nameof(ChangeTurn), 0.5f);
            return;
        }
        _current_turn = GetNextTurn();
        Debug.Log($"Current Turn: {_current_turn}");
        this.turn_change = 1;
        Notify();
        _game_controller_ui_manager.EnableLightBar(_current_turn);
        turn_finished = false;
    }

    // Get the index player that they has turn
    public int GetNextTurn()
    {
        //Debug.Log($"Player count: {_player_count}");
        int temp = _current_turn +  (turn_change * _turn_direction);
        Debug.Log($"Temp turn: {temp}");
        if (temp < 0)
        {
            temp += (_enemy_count + 1);
        }
        temp %= (_enemy_count + 1);
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
                CardSpawner.Instance.Despawn(card.transform);
                card.gameObject.SetActive(false);
                _list_card_played.Add(card); // dùng khi mà có tính năng xem các lá bài đã chơi
                SetCurrentAttributes(card);
                turn_finished = true;
            });


        }
    }

    #endregion

    #region Set Attributes
    public void SetCurrentAttributes(GameObject card)
    {
        if (!card.TryGetComponent<BaseCard>(out var baseCard))
        {
            Debug.LogError($"BaseCard missing on {card.name}");
            return;
        }
        Debug.Log($"Origin color {baseCard.Color}");
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
        Debug.Log($"Card Color {card_color}");
        if(card_color == CardColor.Black)
        {
            Debug.Log($"Black Card and Turn {_current_turn}");
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
        card.SetParent(parent, false);      

        // Sao chép toàn bộ thông số RectTransform
        card.anchorMin = parent.anchorMin;
        card.anchorMax = parent.anchorMax;
        card.pivot = parent.pivot;
        card.sizeDelta = parent.sizeDelta;
        card.anchoredPosition = parent.anchoredPosition;   
        card.localPosition = parent.localPosition;     
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

    public void Notify()
    {
        //Debug.Log($"Notify current turn: {_current_turn}");
        _list_observer[_current_turn].Notify();
    }
    #endregion

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
    }

    public void EndMatch(int player_id)
    {
        //Debug.Log($"Player {player_id} win");
        if (player_id == 0)
        {
            _game_controller_ui_manager.OpenWinPanel();
        }
        else
        {
            _game_controller_ui_manager.OpenLossPanel();
        }
        
    }

    #region Reset Game
    public void DespawnBot()
    {
        for(int i = 1; i < _list_player.Count; i ++)
        {
            EnemySpawner.Instance.Despawn(_list_player[i].transform);
        }
        //_list_player.Clear();
    }

    public void ResetGameController()
    {
        StopAllCoroutines();
        DespawnBot();

        _list_observer?.Clear();
        _list_player?.Clear();
        _list_card_played?.Clear();
        _card_in_deck_remain?.Clear();
        Debug.Log("GameController has been reset.");
    }

    #endregion

    public void BackToHome()
    {
        _on_back_home_game_event?.RaiseEvent();
        ResetGameController();
        StartCoroutine(SwitchSceneCoroutine());
    }

    IEnumerator SwitchSceneCoroutine()
    {

        AsyncOperation loadOp = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        while (!loadOp.isDone) yield return null;

        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync("SampleScene");
        while (!unloadOp.isDone) yield return null;
    }
}
