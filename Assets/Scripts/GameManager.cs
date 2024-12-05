using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Set up singleton for access to Game Manager in outside scripts without referencing it.
    private static GameManager _instance;
    public static GameManager Instance
    {
        get => _instance;
        set
        {
            if (_instance)
            {
                Debug.LogWarning("Multiple Game Managers in scene, delete one.");
                Destroy(value);
                return;
            }

            _instance = value;
        }
    }

    public bool gameActive = true;
    public float elapsedTime = 0f;

    public EndScreenGui endScreen;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame(bool success = false)
    {
        gameActive = false;
        Time.timeScale = 0f;

        if (!success)
            endScreen.UpdateText();

        endScreen.ShowScreen();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
