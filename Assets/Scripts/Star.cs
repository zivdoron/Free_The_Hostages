using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Spike
{
    [SerializeField] float expandingPace;
    [SerializeField] float maxSize = 1;
    private void Update()
    {
        transform.localScale += Vector3.one * expandingPace;
        if(transform.localScale.magnitude > maxSize)
        {
            Destroy(gameObject);
        }
    }
}
