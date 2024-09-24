using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


}