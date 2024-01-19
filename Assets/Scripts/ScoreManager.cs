using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    List<Circle> freedCircles = new List<Circle>();
    List<Circle> missedCircles = new List<Circle>();

    int circlesInRoom;
    private void Start()
    {
        CountCircles();
        instance = this;
    }

    public void AddMissedCircle(Circle circle)
    {
        if(!missedCircles.Exists(c => c == circle))
        {
            missedCircles.Add(circle);
        }
    }
    public void AddFreedCircle(Circle circle)
    {
        if (!freedCircles.Exists(c => c == circle))
        {
            freedCircles.Add(circle);
        }
        if(freedCircles.Count == circlesInRoom)
        {
            UIManager.instance.winningPanel.SetActive(true);
        }

    }

    void CountCircles()
    {
        circlesInRoom = FindObjectsOfType(typeof(Circle)).Length;
        Debug.Log("circles found in room: " + circlesInRoom);
    }
}
