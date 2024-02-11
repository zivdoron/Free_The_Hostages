using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSpike : MovingSpike,IRoomElement
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool rightDir = false;
    [SerializeField] float speed = .05f;

    bool paused = true;
    public bool Paused => paused;

    private void OnEnable()
    {
        Room.OnNewRoom += () => Room.instance.Register(this);
    }
    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (rightDir)
                rb.MovePosition(transform.position + Vector3.right * speed);
            if (!rightDir)
                rb.MovePosition(transform.position + Vector3.left * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Spike"))
        {
            if (manager != null)
                manager.NotifyCollision();
            else
                rightDir = !rightDir;
        }
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

    public override void ChangeDirection()
    {
        rightDir = !rightDir;
    }
}
