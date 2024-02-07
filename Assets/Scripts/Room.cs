using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour, ILevelElement
{
    public static Room instance;
    
    [SerializeField] Wall wall;
    public Transform freedCirclesLocation;

    List<IRoomElement> roomElements = new List<IRoomElement>();
    private void Awake()
    {
        
        instance = this;
    }
    private void Start()
    {
        LevelManager.instance.Register(this);
    }
    public void StartLevel()
    {
        Debug.Log("room starts");
        roomElements.ForEach(e => e.StartAction());
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
    public void NotifyFinishExtend(ExtendingSpike spike)
    {
        if (roomElements.IndexOf(spike) + 1 == roomElements.Count)
            return;
        roomElements[(roomElements.IndexOf(spike) + 1)].StartAction();
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
