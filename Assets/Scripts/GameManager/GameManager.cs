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

    [SerializeField] private Animator transitionAnimator;

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
        scenes = new List<string> { "MainMenu", "IntroScene", "Level 1 tutorial", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "EndScene" };
        currentLevel = 0;
        ChangeState(GameState.MainMenu);
    }

    private void Update()
    {
        // Hotkeys for testing
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    ChangeState(GameState.GameLose);
        //    Debug.Log("Game Lose triggered");
        //}

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    ChangeState(GameState.LevelComplete);
        //    Debug.Log("Level Complete triggered");
        //}

        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    ChangeState(GameState.NewGame);
        //    Debug.Log("New Game triggered");
        //}
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                GoToMainMenu();
                break;
            case GameState.Intro:
                StartIntro();
                break;
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

    private void GoToMainMenu()
    {
        currentLevel = 0;
        StartCoroutine(PlayWithTransition(currentLevel));
    }

    private void StartIntro()
    {
        currentLevel = 1; // IntroScene
        StartCoroutine(PlayWithTransition(currentLevel));
    }

    private void StartTutorial()
    {
        gameplaySO = Instantiate(defaultStatSO);
        gameplaySO.leaf = 0;
        currentLevel = 2; // Level 1 tutorial
        StartCoroutine(PlayWithTransition(currentLevel));
    }

    private void StartNewGame()
    {
        if (progressSO != null)
        {
            gameplaySO = Instantiate(progressSO);
        }
        else
        {
            gameplaySO = Instantiate(defaultStatSO);
        }
        gameplaySO.currentHP = gameplaySO.maxHP;
        currentLevel = 3; // Start at Level 1
        StartCoroutine(PlayWithTransition(currentLevel));
    }

    private void HandleGameLose()
    {
        SaveProgress();
        DataHolder.items.Clear();  // Clear the item list
        StartCoroutine(PlayWithTransition("GameOver"));
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
            StartCoroutine(PlayWithTransition(currentLevel));
        }
    }

    private void HandleGameWin()
    {
        Debug.Log("Game Won! Returning to Main Menu");
        currentLevel = 0;
        StartCoroutine(PlayWithTransition("MainMenu"));
    }

    private IEnumerator PlayWithTransition(int level)
    {
        // Trigger the "End" animation
        transitionAnimator.SetTrigger("End");

        // Wait for the animation to finish
        yield return new WaitForSeconds(1f);  // Adjust time if needed

        // Load the scene
        SceneManager.LoadScene(scenes[level]);

        // Trigger the "Start" animation
        transitionAnimator.SetTrigger("Start");

        // Wait for the animation to finish
        yield return new WaitForSeconds(1f);  // Adjust time if needed
    }

    private IEnumerator PlayWithTransition(string sceneName)
    {
        // Trigger the "End" animation
        transitionAnimator.SetTrigger("End");

        // Wait for the animation to finish
        yield return new WaitForSeconds(1f);  // Adjust time if needed

        // Load the scene
        SceneManager.LoadScene(sceneName);

        // Trigger the "Start" animation
        transitionAnimator.SetTrigger("Start");

        // Wait for the animation to finish
        yield return new WaitForSeconds(1f);  // Adjust time if needed
    }

    public void SaveProgress()
    {
        progressSO = Instantiate(gameplaySO);
        Debug.Log("Progress saved.");
    }
}

public enum GameState
{
    MainMenu,
    Intro,
    Tutorial,
    NewGame,
    GameLose,
    LevelComplete,
    GameWin
}
