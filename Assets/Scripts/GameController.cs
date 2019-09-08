using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : SerializedMonoBehaviour
{
    public static GameController Instance;

    [Header("Configuration")]
    public GameObject MainUI;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        currentlyLoadedScene = SceneManager.GetSceneByName("Workbench");

    }

    void Start()
    {
        MainUI.SetActive(true);
    }

    #region Ingredient Properties
    
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
    public Dictionary<IngredientType, IngredientProperty> IngredientProperties = new Dictionary<IngredientType, IngredientProperty>()
    {
        { IngredientType.FlyAmanita, new IngredientProperty() },
        { IngredientType.MilkWeed, new IngredientProperty() },
        { IngredientType.Powder, new IngredientProperty() },
    };

    public Sprite GetSprite(IngredientType type)
    {
        return IngredientProperties[type].Sprite;
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
                obj.Hide(true);
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
    #endregion
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