using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject winningPanel;

    [SerializeField] GameObject restartPanel;

    private void Start()
    {
        instance = this;
    }
    public void ShowRestartPanel(bool show)
    {
        restartPanel.SetActive(show);
    }
}
