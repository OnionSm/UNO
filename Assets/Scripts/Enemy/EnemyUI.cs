using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _card_left_text;
    public TextMeshProUGUI Card_Text_Left
    {
        get { return _card_left_text; }
        set { _card_left_text = value;}
    }
    
    [SerializeField] private TextMeshProUGUI _cash_text;
    public TextMeshProUGUI Cash_Text
    {
        get { return _cash_text; }
        set { _cash_text = value; }
    }

    public void SetCardLeftText(int value)
    {
        _card_left_text.text = value.ToString();
    }
    public void SetCashText(int value)
    {
        _cash_text.text = value.ToString();
    }
}
