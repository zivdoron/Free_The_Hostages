using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour, ILevelElement
{

    [SerializeField] GameObject panel;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;

    float timeUntilCompletion;
    float timePast;
    float TimeRemaining { get => timeUntilCompletion - timePast; }

    bool paused = false;
    bool running = false;
    bool Running { get => running && !paused; }
    
    private void Start()
    {
        LevelManager.instance.Register(this);
    }
    private void Update()
    {
        if (Running)
        {
            timePast += Time.deltaTime;
            image.fillAmount = 1 - (timePast / timeUntilCompletion);
            text.text = ShowMinutes() + (int)TimeRemaining % 60;
            if(TimeRemaining < 0)
            {
                StopClock();
                LevelManager.instance.EndLevel(false);
            }
        }
    }
    string ShowMinutes()
    {
        if((int)TimeRemaining / 60 > 0)
        {
            return (int)TimeRemaining / 60 + ":";
        }
        return string.Empty;
    }
    public void StartClock(float time)
    {
        ResetClock();
        timeUntilCompletion = time;
        paused = false;
        running = true;
    }
    public void StopClock()
    {
        paused = false;
        running = false;
        ResetClock();
    }
    public void PauseClock()
    {
        paused = true;
    }
    public void ContinueClock()
    {
        if (!paused)
            return;
        paused = false;
    }
    void ResetClock()
    {
        timeUntilCompletion = 0;
        timePast = 0;
        image.fillAmount = 0;
        paused = true;
    }
    public void Hide()
    {
        panel.SetActive(false);
    }
    public void Show()
    {
        panel.SetActive(true);
    }

    public void StartLevel()
    {
        StartClock(LevelManager.instance.CompletionTime);
    }
    public void EndLevel()
    {
        ResetClock();
    }

    public void Pause()
    {
        PauseClock();
    }

    public void Continue()
    {
        ContinueClock();
    }
}
