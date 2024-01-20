using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    [SerializeField] Camera camera;
    Circle circle;
    void Update()
    {
        if(Input.touchCount < 1)
        {
            return;
        }
        print(Input.touches[0].phase);
        if(Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(camera.ScreenToWorldPoint( Input.GetTouch(0).position), Vector2.zero, ContactFilter2D.NormalAngleUpperLimit);
            if(hits != null)
                if(hits.Length > 0)
                {
                    for (int i = 0; i < hits.Length; i++)
                    {
                        if (!hits[i].collider.gameObject.CompareTag("Circle"))
                        {
                            continue;
                        }
                        hits[i].collider.gameObject.TryGetComponent(out circle);
                        
                    }
                }
        }
        if(Input.GetTouch(0).phase == TouchPhase.Moved && circle != null)
        {
            circle.gameObject.transform.position = camera.ScreenToWorldPoint(Input.GetTouch(0).position) + Vector3.forward * 10;
        }
        if(Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            circle = null;
        }
    }
}