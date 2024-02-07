using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendingSpike : MonoBehaviour, IRoomElement
{

    [SerializeField] float scaleSpeed = .005f;
    [SerializeField] float offset = .2f;

    Vector3 startingSize;

    bool paused = false;
    public bool Paused => paused;

    private void Start()
    {
        RegisterToRoom();
    }
    public void RegisterToRoom()
    {
        Room.instance.Register(this);
    }

    public void Extend()
    {
        StartCoroutine(Extension());
    }
    IEnumerator Extension()
    {
        startingSize = transform.localScale;
        while(transform.localScale.x < (startingSize + Vector3.right * offset).x)
        {
            if (this == null)
                break;
            if(!paused)
                transform.localScale += Vector3.right * scaleSpeed;
            yield return new WaitForFixedUpdate();
        }
        EndExtension();
    }
    void EndExtension()
    {
        Room.instance.NotifyFinishExtend(this);
    }

    public void Pause()
    {
        paused = true;
    }

    public void Continue()
    {
        paused = false;
    }

    public void StartAction()
    {
        Extend();
    }
}
public delegate void OnAction();