using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour, IDrawable
{
    [SerializeField] private EnemyUI _enemy_ui;
    [SerializeField] private RectTransform _card_pos;
    public RectTransform Card_Pos
    {
        get { return _card_pos; }
        set { _card_pos = value; }
    }
    public List<Transform> _list_card_in_hand { get; set; }

    void Start()
    {
        CheckCardsInHand();
    }

    void CheckCardsInHand()
    {
        if (_list_card_in_hand.Count < 7)
        {
            Invoke(nameof(CheckCardsInHand), 0.25f);  
        }
        _enemy_ui.SetCardLeftText(_list_card_in_hand.Count);
    }

    public void Draw(int amount)
    {
        _enemy_ui.SetCardLeftText(_list_card_in_hand.Count);
    }
}
