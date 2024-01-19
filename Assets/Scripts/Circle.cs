using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] float shrinkPerFrame;

    bool isFree;
    private void FixedUpdate()
    {
        if (!isFree)
        {
            transform.localScale -= Vector3.one * shrinkPerFrame;
            if (transform.localScale.magnitude < 0.5)
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
            Disappear();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            print("Collided with Room");
            FreeCircle();
        }
    }

    void FreeCircle()
    {
        ScoreManager.instance.AddFreedCircle(this);
        isFree = true;
    }
    void Disappear()
    {
        ScoreManager.instance.AddMissedCircle(this);
        Destroy(gameObject);
    }
}
