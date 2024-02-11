using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikesmanager : MonoBehaviour,IRoomElement
{
    [SerializeField] List<MovingSpike> spikes;

    bool paused = false;
    public bool Paused => paused;
    private void OnEnable()
    {
        Room.OnNewRoom += () => Room.instance.Register(this);
    }
    public void Continue()
    {
        paused = false;
    }

    public void EndAction()
    {
        paused = true;
    }

    public void NotifyCollision()
    {
        spikes.ForEach(spike => spike.ChangeDirection());
    }

    public void Pause()
    {
        paused = true;
    }

    public void StartAction()
    {
        paused = false;
        spikes.ForEach(s => s.AssignManager(this));
    }
}
