using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Circle : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] CircleAnimator circleAnimator;
    Rigidbody2D rb;

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

    public void FreeCircle()
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

    public void OnPointerEnter(PointerEventData eventData)
    {
    }
}
