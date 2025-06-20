using DG.Tweening;
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

    private float kMoveDuration = 2f;  
    private float kRotateDuration = 1f; 
    private float kEpsilon = 1f;  


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
        
        if (state)
        {
            //UnityEngine.Debug.Log($"Card Selected. Call Stack: \n{stackTrace}");
            //Debug.Log($"Card selected {state}");
            transform.DOMove(transform.position + new Vector3(0, 1, 0), 0.2f);
        }
        else
        {
            //UnityEngine.Debug.Log($"Card Unselected  Call Stack: \n{stackTrace}");
            //Debug.Log($"Card unselected {state}");
            transform.DOMove(transform.position - new Vector3(0, 1, 0), 0.2f);
        }
    }

    public Sequence PlayCardAnimation(RectTransform destination)
    {
        // ----------------------------------------------------------------
        // 0. Chuẩn bị reference & anchor / size cố định
        // ----------------------------------------------------------------
        RectTransform modelRect = (RectTransform)transform;          // CardModel
        RectTransform cardRect = (RectTransform)transform.parent;   // Card (cha)

        Canvas rootCanvas = cardRect.GetComponentInParent<Canvas>().rootCanvas;
        RectTransform canvasRt = (RectTransform)rootCanvas.transform;

        Camera uiCam = rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay
                       ? null
                       : rootCanvas.worldCamera;

        // Anchor vào giữa và gán kích thước cố định 115 × 170 (pixel)
        Vector2 center = new Vector2(0.5f, 0.5f);
        modelRect.anchorMin = center;
        modelRect.anchorMax = center;
        modelRect.anchoredPosition = Vector2.zero;

        modelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 115f);
        modelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 170f);

        // ----------------------------------------------------------------
        // 1. Tính vị trí card & đích (đưa về local-space của rootCanvas)
        // ----------------------------------------------------------------
        Vector2 cardScr = RectTransformUtility.WorldToScreenPoint(uiCam, cardRect.position);
        Vector2 destScr = RectTransformUtility.WorldToScreenPoint(uiCam, destination.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRt, cardScr, uiCam, out Vector2 cardLocal);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRt, destScr, uiCam, out Vector2 destLocal);

        // ----------------------------------------------------------------
        // 2. Chuyển Card (cha) về chung rootCanvas để tween
        // ----------------------------------------------------------------
        Transform oldParent = cardRect.parent;
        cardRect.SetParent(canvasRt, true);   // giữ world-pos/scale
        cardRect.anchoredPosition = cardLocal;

        // ----------------------------------------------------------------
        // 3. Tạo sequence (chỉ vị trí + xoay lật nếu cần)
        // ----------------------------------------------------------------
        const float kMoveDur = 0.35f;
        const float kRotDur = 0.25f;
        const float kEps = 3f;

        bool needFlip = Mathf.Abs(Mathf.Repeat(modelRect.eulerAngles.y, 360f) - 180f) < kEps;

        Sequence seq = DOTween.Sequence()
            .Append(cardRect.DOAnchorPos(destLocal, kMoveDur).SetEase(Ease.OutQuad));

        if (needFlip)
            seq.Join(modelRect.DORotate(Vector3.zero, kRotDur, RotateMode.FastBeyond360));

        // ----------------------------------------------------------------
        // 4. Hoàn tất – trả hierarchy
        // ----------------------------------------------------------------
        seq.onComplete += () =>
        {
            cardRect.SetParent(oldParent, true);  // hoặc SetParent(destination, false);
        };

        return seq;
    }
}
