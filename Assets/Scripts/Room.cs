using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] Wall wall;

    [SerializeField] List<Wall> walls;

    
    private void Start()
    {
        
    }

    //void SpawnWalls()
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        walls.Add(Instantiate(wall, transform.position + Vector3.left * (transform.localScale.x * 0.5f), Quaternion.identity));
    //    }
    //}
}
