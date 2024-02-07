using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSpike : Spike,IRoomElement
{
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
                transform.position += Vector3.right * speed;
            if (!rightDir)
                transform.position += Vector3.left * speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Spike"))
        {
            rightDir = !rightDir;
        }
    }

    public void StartAction()
    {
        paused = false;
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
