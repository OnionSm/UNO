using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [Header("UI")]

    [Header("Game Event")]
    [SerializeField] private CardEvent _on_valid_card_ev;
    public bool _card_selected { get; set; } = false;
    public int _player_id { get; set; } = -1;
    // Start is called before the first frame update
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
        Debug.Log("Check card called");
        _on_valid_card_ev?.RaiseEvent(card.Color, card.Type, card.Symbol);
    }
    public void OnCardSelection()
    {
        if (_player_id == 0)
        {
            
        }
    }
}
