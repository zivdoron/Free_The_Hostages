using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour, IRoomElement
{
    [SerializeField] CircleAnimator circleAnimator;
    public Rigidbody2D rb;

    [SerializeField] float shrinkPerFrame;
    [SerializeField] float minSize;

    bool isFree = false;
    bool isDisappearing = false;

    bool paused = true;
    public bool Paused => paused;

    bool isDead = false;
    public bool IsDead { get => isDead; }

    private void FixedUpdate()
    {
        if (!isFree && !paused)
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
        //print("collided");
        if (collision.gameObject.CompareTag("Spike"))
        {
            print("collided with spike");
            isDead = true;
            StartDisappearingSession();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            if (!isFree && !isDead)
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

    

    public void StartAction()
    {
        paused = false;
    }
    public void EndAction()
    {
        paused = true;
    }

    public void Pause()
    {
        paused = true;
    }

    public void Continue()
    {
        paused = false;
    }
}
