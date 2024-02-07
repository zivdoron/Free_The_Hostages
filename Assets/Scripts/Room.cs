using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour, ILevelElement
{
    public static Room instance;

    public static OnAction OnNewRoom = () => { };
    
    [SerializeField] Wall wall;
    public Transform freedCirclesLocation;

    List<IRoomElement> roomElements = new List<IRoomElement>();
    List<ExtendingSpike> extendingSpikes = new List<ExtendingSpike>();
    private void OnEnable()
    {
        instance = this;
        LevelManager.OnLevelStart += () => LevelManager.instance.Register(this);
        Invoke("InvokeNewRoom", 0.5f);
    }
    void InvokeNewRoom()
    {
        OnNewRoom.Invoke();
    }
    
    public void StartLevel()
    {
        Debug.Log("room starts");
        print("elements in room: " + roomElements.Count);
        roomElements.ForEach(e => e.StartAction());
        if (extendingSpikes.Count > 0)
            extendingSpikes[0].Extend();
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Circle"))
    //    {
    //        print("Collided with Room");

    //        collision.gameObject.GetComponent<Circle>().FreeCircle();

    //    }
    //}
    public void Register(IRoomElement element)
    {
        Debug.Log("room element registered");
        roomElements.Add(element);
    }
    public void SpikeRegister(ExtendingSpike spike)
    {
        if (!extendingSpikes.Exists(s => s == spike))
            extendingSpikes.Add(spike);
    }
    public void NotifyFinishExtend(ExtendingSpike spike)
    {
        if (extendingSpikes.IndexOf(spike) + 1 == extendingSpikes.Count)
            return;
        extendingSpikes[(extendingSpikes.IndexOf(spike) + 1)].Extend();
    }

    public void Pause()
    {
        roomElements.ForEach(e => e.Pause());
    }
    public void Continue()
    {
        roomElements.ForEach(e => e.Continue());
    }




    //void SpawnWalls()
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        walls.Add(Instantiate(wall, transform.position + Vector3.left * (transform.localScale.x * 0.5f), Quaternion.identity));
    //    }
    //}
}
public interface IRoomElement
{
    bool Paused { get; }
    public void StartAction();
    public void Pause();
    public void Continue();

}
