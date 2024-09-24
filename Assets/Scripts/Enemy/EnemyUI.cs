using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _card_left_text;

    public void SetCardLeftText(int value)
    {
        _card_left_text.text = value.ToString();
    }
}
