using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : SerializedMonoBehaviour
{
    public static GameController Instance;

    [Header("Configuration")]
    public GameObject MainUI;
    public string[] ManagedScenes;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        MainUI.SetActive(true);

        // Setup scenes dynamically
        int targetScene = 0;
        foreach (var sceneName in ManagedScenes)
        {
            var scene = SceneManager.GetSceneByName(sceneName);

            if (!scene.IsValid())
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
            else if (currentlyLoadedScene.buildIndex == -1)
            {
                targetScene = scene.buildIndex;
            }
        }
        SwitchScene(targetScene);
    }

    #region Ingredient Properties
    
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
    public Dictionary<IngredientType, Sprite> IngredientSprites = new Dictionary<IngredientType, Sprite>()
    {
    };

    public Sprite GetSprite(IngredientType type)
    {
        return IngredientSprites[type];
    }

    #endregion

    #region Scene Management
    [HideInInspector]
    public Scene currentlyLoadedScene;
    Dictionary<Scene, List<SceneObject>> SceneObjects = new Dictionary<Scene, List<SceneObject>>();
    public static bool IsInCurrentScene(GameObject go) => go.scene == Instance.currentlyLoadedScene;

    public void SwitchScene(int targetSceneIndex)
    {
        Scene targetScene = SceneManager.GetSceneByBuildIndex(targetSceneIndex);
        if (targetScene == currentlyLoadedScene) return;

        var previousScene = currentlyLoadedScene;
        currentlyLoadedScene = targetScene;

        // Turn off visuals in previous scene
        if (SceneObjects.ContainsKey(previousScene))
        {
            foreach (var obj in SceneObjects[previousScene])
            {
                obj.Hide();
            }
        }

        SceneManager.SetActiveScene(currentlyLoadedScene);

        // Turn on visuals in new scene
        if (SceneObjects.ContainsKey(currentlyLoadedScene))
        {
            foreach (var obj in SceneObjects[currentlyLoadedScene])
            {
                obj.Show();
            }
        }
    }

    public void Register(SceneObject obj)
    {
        if (!SceneObjects.ContainsKey(obj.gameObject.scene))
        {
            SceneObjects.Add(obj.gameObject.scene, new List<SceneObject>());
        }
        var list = SceneObjects[obj.gameObject.scene];
        if (!list.Contains(obj))
        {
            SceneObjects[obj.gameObject.scene].Add(obj);
        }

        if (!IsInCurrentScene(obj.gameObject))
        {
            obj.Hide();
        }
    }

    public void Unregister(SceneObject obj)
    {
        var scene = obj.gameObject.scene;
        if (SceneObjects.ContainsKey(scene))
        {
            SceneObjects[scene].Remove(obj);
        }
    }
    #endregion

    #region Recipes
    public List<PropertyInteraction> Interactions;
    public List<PropertyResults> Results;
    #endregion Recipes
}

[Flags]
public enum IngredientType
{
    MilkWeed = 1,
    FlyAmanita = 2,
    ArrowRoot = 4,
    BloodGrass = 8,
    Lavender = 16,
    ToadLegs = 32,
    Powder = 64,
    Bottle = 128
}