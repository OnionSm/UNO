using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameControllerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _card_amount_text;
    // Start is called before the first frame update
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
    
}
