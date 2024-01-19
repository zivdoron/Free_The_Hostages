using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject winningPanel;
    private void Start()
    {
        instance = this;
    }

}
