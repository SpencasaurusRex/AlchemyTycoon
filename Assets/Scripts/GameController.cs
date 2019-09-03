using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : SerializedMonoBehaviour
{
    public static GameController Instance;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        CurrentlyLoadedScene = SceneManager.GetSceneByName("Workbench");

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

    public Scene CurrentlyLoadedScene;
    Dictionary<Scene, List<SceneObject>> SceneObjects = new Dictionary<Scene, List<SceneObject>>();
    public static bool CurrentScene(GameObject go) => go.scene == Instance.CurrentlyLoadedScene;

    public void SwitchScene(int targetSceneIndex)
    {
        Scene targetScene = SceneManager.GetSceneByBuildIndex(targetSceneIndex);
        if (targetScene == CurrentlyLoadedScene) return;

        var previousScene = CurrentlyLoadedScene;
        CurrentlyLoadedScene = targetScene;

        // Turn off visuals in previous scene
        if (SceneObjects.ContainsKey(previousScene))
        {
            foreach (var obj in SceneObjects[previousScene])
            {
                obj.Hide(true);
            }
        }

        // Turn on visuals in new scene
        if (SceneObjects.ContainsKey(CurrentlyLoadedScene))
        {
            foreach (var obj in SceneObjects[CurrentlyLoadedScene])
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