using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class CardModel : MonoBehaviour
{
    [Header("Card Protecter")]
    [SerializeField] private Sprite _card_protecter;
    public Sprite card_protecter
    {
        get { return _card_protecter; }
        set { _card_protecter = value;}
    }

    [Header("Card Image")]
    [SerializeField] private Sprite _card_image;
    public Sprite card_image
    {
        get { return _card_image;}
        set { _card_image = value;}
    }

    [Header("Card Object")]
    [SerializeField] private Image _card_image_holder;


    [SerializeField] private CardAnimation _card_animation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadComponent()
    {
        this._card_image_holder.sprite = _card_protecter;
    }
    public void StartFlipDown()
    {
        _card_animation.SetBoolFlipDown(true);
    }
    public void iSFlipDown()
    {
        _card_image_holder.sprite = _card_protecter;
    }
    public void EndFlipDown()
    {
        _card_animation.SetBoolFlipDown(false);
    }
    public void StartFlipUp()
    {
        _card_animation.SetBoolFlipUp(true);
    }
    public void isFlipUp()
    {
        _card_image_holder.sprite = _card_image;
    }
    public void EndFlipUp()
    {
        _card_animation.SetBoolFlipUp(false);
    }
    public void SetCardStateUI(bool state)
    {
        StackTrace stackTrace = new StackTrace();
        
        if (state)
        {
            UnityEngine.Debug.Log($"Card Selected. Call Stack: \n{stackTrace}");
            //Debug.Log($"Card selected {state}");
            transform.DOMove(transform.position + new Vector3(0, 1, 0), 0.2f);
        }
        else
        {
            UnityEngine.Debug.Log($"Card Unselected  Call Stack: \n{stackTrace}");
            //Debug.Log($"Card unselected {state}");
            transform.DOMove(transform.position - new Vector3(0, 1, 0), 0.2f);
        }
    }

}
