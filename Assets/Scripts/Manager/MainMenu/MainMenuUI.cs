using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _setting_panel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSettingPanel() 
    {
        _setting_panel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseSettingPanel()
    {
        Time.timeScale = 1f;
        _setting_panel?.SetActive(false);
    }
}
