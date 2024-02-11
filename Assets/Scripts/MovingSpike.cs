using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class MovingSpike : Spike
{
    protected Spikesmanager manager;
    public abstract void ChangeDirection();
    public void AssignManager(Spikesmanager spikesmanager)
    {
        manager = spikesmanager;
    }
}
