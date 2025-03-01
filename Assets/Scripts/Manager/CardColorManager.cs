using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardColorManager : MonoBehaviour
{
    public Dictionary<CardColor, string> color_mapping = new Dictionary<CardColor, string>();
    [SerializeField] private Image _color_panel;


    // Start is called before the first frame update
    void Start()
    {
        this.LoadComponent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadComponent()
    {
        List<ColorConfig> all_color_configs  = GameManager.Instance.GetColorConfigs();
        foreach(ColorConfig config in all_color_configs)
        {
            color_mapping[config.type_color] = config.color;
        }
    }
    public void UpdateColorPanel(CardColor card_color)
    {
        string hex_color = color_mapping[card_color];
        if (UnityEngine.ColorUtility.TryParseHtmlString(hex_color, out Color color))
        {
            _color_panel.color = color; 
        }
    }
    
}
