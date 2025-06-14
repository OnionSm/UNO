using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    
    [Header("UI")]
    [SerializeField] private CardModel _card_model;    

    [Header("Game Event")]
    [SerializeField] private CardEvent _on_valid_card_ev;
    [SerializeField] private BoolEvent _on_available_play_card_btn_ev;
    [SerializeField] private GameEvent _on_unselect_card_ev;




    public bool _card_selected { get; set; } = false;
    public int _player_id { get; set; } = -1;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckCardValid()
    {
        BaseCard card = gameObject.GetComponentInChildren<BaseCard>();
        if(card == null)
        {
            Debug.Log("base card nullllllllllllllllllllllllll");
        }
        _on_valid_card_ev?.RaiseEvent(gameObject, card.Color, card.Type, card.Symbol);
    }
    public void OnCardSelection()
    {
        if (_player_id == 0)
        {
            if(!_card_selected)
            {
                _card_selected = true;
                _card_model.SetCardStateUI(true);
                CheckCardValid();
            }
            else
            {
                _card_selected = false;
                _card_model.SetCardStateUI(false);
                _on_unselect_card_ev?.RaiseEvent();
                _on_available_play_card_btn_ev?.RaiseEvent(false);
            }
            
        }
    }
}
