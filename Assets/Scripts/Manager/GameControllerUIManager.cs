using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerUIManager : MonoBehaviour
{
    private int _alpha_enable_button = 255;
    private int _alpha_unenable_button = 100;
    [SerializeField] private TextMeshProUGUI _card_amount_text;
    [SerializeField] private Button _play_button;
    [SerializeField] private Button _drop_turn_button;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCardAmountText(int card_amount)
    {
        _card_amount_text.text = $"{card_amount}";
    }
    public void EnablePlayCardButton()
    {
        Image image = _play_button.GetComponent<Image>();
        Color new_color = image.color;
        new_color.a = _alpha_enable_button / 255;
        image.color = new_color;
    }
    public void DisablePlayCardButton()
    {
        Image image = _play_button.GetComponent<Image>();
        Color new_color = image.color;
        new_color.a = _alpha_unenable_button / 255;
        image.color = new_color;
    }
    public void DisplayPlayCardButton()
    {
        _play_button.gameObject.SetActive(true);
    }
    public void UnDisplayCardButton()
    {
        _play_button.gameObject.SetActive(false);
    }
    public void DisplayDropTurnButton()
    {
        _drop_turn_button.gameObject.SetActive(true);
    }
    public void UnDisplayDropTurnButton()
    {
        _drop_turn_button.gameObject.SetActive(false);
    }

    
}
