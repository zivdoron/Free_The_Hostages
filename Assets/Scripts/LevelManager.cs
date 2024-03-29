using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] LevelData[] levels;
    int currentLevel = 1;
    bool levelInProgress = false;
    public bool LevelInProgress { get => levelInProgress; }

    public static OnAction OnLevelStart = () => { };
    //public static OnAction OnLose = () => { };
    //public static OnAction OnWin = () => { };

    
    public float CompletionTime { get => levels[currentLevel - 1].completionTime; }

    List<ILevelElement> levelElements = new List<ILevelElement>();

    private void OnEnable()
    {
        instance = this;
    }
    public void Pause()
    {
        levelElements.ForEach(e => e.Pause());
    }
    public void StartLevel()
    {
        SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive).completed += async =>
        Invoke("StartLevelInternal", 1f);
    }
    void StartLevelInternal()
    {
        levelInProgress = true;
        print("elements in level: " + levelElements.Count);
        
        OnLevelStart.Invoke();
        ClearNulls();
        levelElements.ForEach(e => e.StartLevel());
    }
    public void Restart()
    {
        SceneManager.UnloadSceneAsync(currentLevel);
        StartLevel();
    }
    public void LevelUp()
    {
        SceneManager.UnloadSceneAsync(currentLevel);
        currentLevel++;
        StartLevel();
    }
    public void EndLevel(bool win)
    {
        levelInProgress = false;
        ClearNulls();
        levelElements.ForEach(e => e.EndLevel());
        OnLevelStart = () => { };
        if (win)
        {
            UIManager.instance.ShowWinningPanel(true);
        }
        else
        {
            UIManager.instance.ShowRestartPanel(true);
        }

    }
    void ClearNulls()
    {
        levelElements.RemoveAll(e => e == null);
    }

    public void Register(ILevelElement levelElement)
    {
        if(!levelElements.Exists(e => e == levelElement))
            levelElements.Add(levelElement);
    }

    [System.Serializable]
    class LevelData
    {
        public float completionTime;
    }
}

public interface ILevelElement
{
    public void StartLevel();
    public void EndLevel();
    public void Register()
    {
        LevelManager.instance.Register(this);
    }
    public void Pause();
    public void Continue();

}
