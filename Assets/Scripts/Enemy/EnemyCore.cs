using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour, IDrawable
{
    [SerializeField] private EnemyUI _enemy_ui;
    public List<Transform> _list_card_in_hand { get; set; }

    void Start()
    {
        _enemy_ui.SetCardLeftText(_list_card_in_hand.Count);
    }

    public void Draw(int amount)
    {
        _enemy_ui.SetCardLeftText(_list_card_in_hand.Count);
    }
}
