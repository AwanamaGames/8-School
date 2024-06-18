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

        // Check if current scene is "MainMenu" and disable children if true
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        scenes = new List<string> { "MainMenu", "Level 1 tutorial", "Level 1", "Level 2" };
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
        gameplaySO.leaf = 0;
        currentLevel = 1;
        StartCoroutine(PlayWithTransition(currentLevel));
    }

    private void StartNewGame()
    {
        gameplaySO = Instantiate(defaultStatSO);
        gameplaySO.currentHP = gameplaySO.maxHP;
        currentLevel = 1;
        StartCoroutine(PlayWithTransition(currentLevel));
    }

    private void HandleGameLose()
    {
        SaveProgress();
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