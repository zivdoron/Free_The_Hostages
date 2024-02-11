using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSpike : Spike,IRoomElement
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool upDir = false;
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
            if (upDir)
                rb.MovePosition(transform.position + Vector3.up * speed);
            if (!upDir)
                rb.MovePosition(transform.position + Vector3.down * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Spike"))
        {
            upDir = !upDir;
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

}
