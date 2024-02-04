using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] LevelData[] levels;
    int currentLevel = 0;

    public static OnAction OnLevelStart = () => { };
    //public static OnAction OnLose = () => { };
    //public static OnAction OnWin = () => { };

    Scene ActiveScene { get => GetLevel(currentLevel); }
    Scene GetLevel(int levelNum) { return SceneManager.GetSceneByName("Level_" + levelNum); }
    public float CompletionTime { get => levels[currentLevel].completionTime; }

    List<ILevelElement> levelElements = new List<ILevelElement>();

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Invoke("StartLevel", 1);
    }
    public void Pause()
    {
        levelElements.ForEach(e => e.Pause());
    }
    public void StartLevel()
    {
        ClearNulls();
        levelElements.ForEach(e => e.StartLevel());
        OnLevelStart.Invoke();
    }
    public void Restart()
    {
        SceneManager.UnloadScene(GetLevel(currentLevel + 1));
        SceneManager.LoadScene(GetLevel(currentLevel + 1).buildIndex, LoadSceneMode.Additive);
    }
    public void LevelUp()
    {
        SceneManager.UnloadScene(GetLevel(currentLevel + 1));
        currentLevel++;
        SceneManager.LoadScene(GetLevel(currentLevel + 1).buildIndex, LoadSceneMode.Additive);
        StartLevel();
    }
    public void EndLevel(bool win)
    {

    }
    void ClearNulls()
    {
        for (int i = 0; i < levelElements.Count; i++)
        {
            if(levelElements[i] == null)
            {
                levelElements.RemoveAt(i);
                i--;
            }
        }
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
    public void Register()
    {
        LevelManager.instance.Register(this);
    }
    public void Pause();
    public void Continue();

}
