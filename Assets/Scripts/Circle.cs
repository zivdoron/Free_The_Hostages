using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] CircleAnimator circleAnimator;

    [SerializeField] float shrinkPerFrame;
    [SerializeField] float minSize;

    bool isFree = false;
    bool isDisappearing = false;
    private void FixedUpdate()
    {
        if (!isFree)
        {
            transform.localScale -= Vector3.one * shrinkPerFrame;
            if (transform.localScale.magnitude < minSize)
            {
                Disappear();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collided");
        if (collision.gameObject.CompareTag("Spike"))
        {
            print("collided with spike");
            StartDisappearingSession();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            print("Collided with Room");
            if (!isFree)
            {
                FreeCircle();
            }
        }
    }

    void FreeCircle()
    {
        ScoreManager.instance.AddFreedCircle(this);
        isFree = true;
    }

    void StartDisappearingSession()
    {
        isDisappearing = true;
        circleAnimator.StartDisappearingAnim();
    }
    public void Disappear()
    {
        ScoreManager.instance.AddMissedCircle(this);
        Destroy(gameObject);
    }
}
