using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private int _bgm_sound_id = 0;
    // Start is called before the first frame update
    void Start()
    {
        LoadComponent();
        PlayBGMSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void LoadComponent()
    {

    }
    void PlayBGMSound()
    {
        if (AudioManager.Instance.AudioLoaded)
        {
            AudioManager.Instance.PlayBGM(_bgm_sound_id);
        }
        else
        {
            Invoke("PlayBGMSound", 0.5f);
        }
    }

    public void ChooseStage(int stage)
    {
        GameManager.Instance.current_stage = stage;
        GameManager.Instance.SetStageConfig();
        SceneManager.LoadSceneAsync("SampleScene");

        Debug.Log($"Choose stage: {stage}");
    }
}
