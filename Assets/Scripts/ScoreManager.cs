using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    

    List<Circle> freedCircles = new List<Circle>();
    int missedCircles = 0;

    int circlesInRoom;
    private void Start()
    {
        CountCircles();
        instance = this;
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
        circlesInRoom = FindObjectsOfType(typeof(Circle)).Length;
        Debug.Log("circles found in room: " + circlesInRoom);
    }

    void CheckEndCondition()
    {
        if(missedCircles + freedCircles.Count >= circlesInRoom)
        {
            UIManager.instance.ShowRestartPanel(true);
            UIManager.instance.winningPanel.SetActive(true);
        }
    }
}
