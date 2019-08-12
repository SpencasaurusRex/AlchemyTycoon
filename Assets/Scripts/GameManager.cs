using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public WorkBenchData WorkBenchData;
    public PlantData PlantData;
    public StoreData StoreData;

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
            case GameScene.Garden:
                InitializeGardenScene();
                break;
            case GameScene.Workbench:
                InitializeWorkBenchScene();
                break;
            case GameScene.Store:
                InitializeStoreScene();
                break;
        }
    }

    void InitializeMainMenuScene()
    {

    }

    void InitializeGardenScene()
    {

    }

    void InitializeWorkBenchScene()
    {

    }

    void InitializeStoreScene()
    {

    }
}

public enum GameScene
{
    MainMenu,
    Garden,
    Workbench,
    Store
}
