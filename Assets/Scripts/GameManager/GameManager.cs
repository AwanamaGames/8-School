using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public GameState currentState;
    [HideInInspector] public List<string> scenes;
    [HideInInspector] public int currentLevel;

    public StatSO defaultStatSO;
    public StatSO gameplaySO;
    public StatSO progressSO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        scenes = new List<string> { "Level 1", "Level 2"};
        currentLevel = 0;
        ChangeState(GameState.Tutorial);
    }

    private void Update()
    {
        // Hotkeys for testing
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeState(GameState.GameLose);
            Debug.Log("Game Lose triggered");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(GameState.LevelComplete);
            Debug.Log("Level Complete triggered");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeState(GameState.NewGame);
            Debug.Log("New Game triggered");
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case GameState.Tutorial:
                StartTutorial();
                break;
            case GameState.NewGame:
                StartNewGame();
                break;
            case GameState.GameLose:
                HandleGameLose();
                break;
            case GameState.LevelComplete:
                HandleLevelComplete();
                break;
            case GameState.GameWin:
                HandleGameWin();
                break;
        }
    }

    private void StartTutorial()
    {
        gameplaySO = Instantiate(defaultStatSO);
        currentLevel = 0;
        Play(currentLevel);
    }

    private void StartNewGame()
    {
        gameplaySO = Instantiate(defaultStatSO);
        gameplaySO.leaf = 0;
        currentLevel = 0;
        Play(currentLevel);
    }

    private void HandleGameLose()
    {
        SaveProgress();
        currentLevel = 0;
        gameplaySO = Instantiate(progressSO);
        gameplaySO.leaf = 0;
        Play(currentLevel);
    }

    private void HandleLevelComplete()
    {
        currentLevel++;
        if (currentLevel >= scenes.Count)
        {
            ChangeState(GameState.GameWin);
        }
        else
        {
            Play(currentLevel);
        }
    }

    private void HandleGameWin()
    {
        Debug.Log("Game Won! Returning to Main Menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void Play(int level)
    {
        SceneManager.LoadScene(scenes[level]);
    }

    public void SaveProgress()
    {
        progressSO = Instantiate(gameplaySO);
        Debug.Log("Progress saved.");
    }

    //public void LoadProgress()
    //{
    //    if (progressSO != null)
    //    {
    //        gameplaySO = Instantiate(progressSO);
    //        Play(currentLevel);
    //        Debug.Log("Progress loaded.");
    //    }
    //    else
    //    {
    //        Debug.Log("No saved progress found.");
    //    }
    //}
}
