using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[System.Serializable]
public class EnemyUIAttributes 
{
    [Header("Card Amount")]
    public TextMeshProUGUI _card_amount;

    [Header("Enemy Deck")]
    public RectTransform _deck_pos;

    [Header("Cash Amount")]
    public TextMeshProUGUI _cash_amount;
}
