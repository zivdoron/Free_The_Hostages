using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAnimator : MonoBehaviour
{
    public Circle circle;

    [SerializeField] Animator animator;

    public void StartDisappearingAnim()
    {
        animator.Play("Blood");
    }
    public void Disappear()
    {
        circle.Disappear();
    }

}
