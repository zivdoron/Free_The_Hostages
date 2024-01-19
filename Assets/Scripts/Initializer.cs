using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] List<Initializee> initializees;

    private void Start()
    {
        initializees.ForEach(init => init.Init());
    }
}

public interface Initializee
{
    public bool Init();
}
