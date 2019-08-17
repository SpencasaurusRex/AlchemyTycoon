using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        var scene = (GameScene)Enum.Parse(typeof(GameScene), SceneManager.GetActiveScene().name);
        InitializeScene(scene);
    }

    public void SwitchScene(GameScene scene)
    {
        SceneManager.LoadScene(scene.ToString());
        InitializeScene(scene);
    }

    void InitializeScene(GameScene scene)
    {
        switch (scene)
        {
            case GameScene.MainMenu:
                InitializeMainMenuScene();
                break;
            case GameScene.Game:
                InitializeGameScene();
                break;
        }
    }

    void InitializeMainMenuScene()
    {

    }

    void InitializeGameScene()
    {

    }
}

public enum GameScene
{
    MainMenu,
    Game
}
