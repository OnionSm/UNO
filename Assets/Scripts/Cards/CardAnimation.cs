using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform _played_zone;

    private readonly int FlipUp = Animator.StringToHash("FlipUp");
    private readonly int FlipDown = Animator.StringToHash("FlipDown");

    public void SetBoolFlipUp(bool value)
    {
        animator.SetBool(FlipUp, value);
    }
    public void SetBoolFlipDown(bool value)
    {
        animator.SetBool(FlipDown, value);
    }
    public void SetCardClicked(bool value)
    {

    }
    public void PlayCard()
    {
        transform.DOMove(_played_zone.position, 1f).SetEase(Ease.OutQuad);
    }
}
