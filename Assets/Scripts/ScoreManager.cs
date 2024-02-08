using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour,ILevelElement
{
    public static ScoreManager instance;

    

    List<Circle> freedCircles = new List<Circle>();
    int missedCircles = 0;

    int circlesInRoom;
    private void Start()
    {
        instance = this;
        LevelManager.OnLevelStart += () => LevelManager.instance.Register(this);
    }

    public void AddMissedCircle(Circle circle)
    {
        missedCircles++;
        CheckEndCondition();
    }
    public void AddFreedCircle(Circle circle)
    {
        if (!freedCircles.Exists(c => c == circle))
        {
            freedCircles.Add(circle);
            Destroy(circle.GetComponent<Collider2D>());
            circle.transform.position = Room.instance.freedCirclesLocation.position + Vector3.right * 3f * circle.transform.localScale.x * freedCircles.Count;
            circle.rb.velocity = Vector2.zero;
        }
        CheckEndCondition();

    }

    void CountCircles()
    {
        circlesInRoom = Room.instance.GetCircles().Count;
        Debug.Log("circles found in room: " + circlesInRoom);
    }

    void CheckEndCondition()
    {
        if(freedCircles.Count == circlesInRoom)
        {
            LevelManager.instance.EndLevel(true);
        }
        if(missedCircles + freedCircles.Count >= circlesInRoom)
        {
            LevelManager.instance.EndLevel(false);
        }
    }
    public void EndLevel()
    {
        freedCircles.Clear();
        circlesInRoom = 0;
        missedCircles = 0;
    }

    public void StartLevel()
    {
        CountCircles();
    }

    public void Pause()
    {
        
    }

    public void Continue()
    {
        
    }
}
