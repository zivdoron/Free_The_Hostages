using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject winningPanel;

    [SerializeField] GameObject restartPanel;
    [SerializeField] GameObject levelEndsPanel;

    private void Start()
    {
        instance = this;
    }
    public void ShowRestartPanel(bool show)
    {
        restartPanel.SetActive(show);
        levelEndsPanel.SetActive(show);
    }
    public void ShowWinningPanel(bool show)
    {
        winningPanel.SetActive(show);
        levelEndsPanel.SetActive(show);
    }
}
