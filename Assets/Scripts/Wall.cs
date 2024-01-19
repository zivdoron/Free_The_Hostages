using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] Spike spike;

    [SerializeField] Transform spikesSpawnPoint;

    int spikesRate;

    public void SpawnSpikes(int rate)
    {
        for (int i = 0; i < rate; i++)
        {
            Instantiate(spike, spikesSpawnPoint.position + Vector3.up * Random.Range(-transform.localScale.y, transform.localScale.y), Quaternion.Euler(0, 0, transform.localEulerAngles.z), this.gameObject.transform);
        }
    }
}
