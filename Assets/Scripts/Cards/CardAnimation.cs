using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

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
}
