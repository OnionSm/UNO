using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerUIManager : MonoBehaviour
{
    private int _alpha_enable_button = 255;
    private int _alpha_unenable_button = 150;

    [Header("General UI")]
    [SerializeField] private TextMeshProUGUI _card_amount_text;
    [SerializeField] private Button _play_card_button;
    [SerializeField] private Button _drop_turn_button;
    [SerializeField] private TextMeshProUGUI _current_turn_text;

    [Header("Panels")]
    [SerializeField] private Image _color_selection_panel;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        { 
            OpenColorSelectionPanel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            CloseColorSelectionPanel();
        }
    }
    public void SetCardAmountText(int card_amount)
    {
        _card_amount_text.text = $"{card_amount}";
    }

    public void SetAvailablePlayCardButton(bool state)
    {
        Image image = _play_card_button.GetComponent<Image>();
        Color new_color = image.color;
        new_color.a = state ? _alpha_enable_button / 255 : _alpha_unenable_button / 255;
        image.color = new_color;
    }
    public void SetAppearancePlayCardButton(bool state)
    {
        //Debug.Log("Play card called");
        _play_card_button?.gameObject.SetActive(state);
        SetAvailablePlayCardButton(false);

    }
    public void SetAppearanceDropTurnButton(bool state)
    {
        _drop_turn_button?.gameObject.SetActive(state);
    }

    public void SetCurrentTurnText(int current_turn)
    {
        _current_turn_text.SetText(current_turn.ToString());
    }
    
    public void OpenColorSelectionPanel()
    {
        _color_selection_panel.gameObject.SetActive(true);
    }

    public void CloseColorSelectionPanel()
    {
        _color_selection_panel?.gameObject.SetActive(false);
    }

    
}
