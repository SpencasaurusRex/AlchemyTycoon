﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    
    public Dictionary<Type, List<Functionality>> Functionalities = new Dictionary<Type, List<Functionality>>();
    public Dictionary<Type, List<UnityEngine.Object>> Objects = new Dictionary<Type, List<UnityEngine.Object>>();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Register logic
        Register(new HideObject());

        // Register existing objects
        foreach (var kvp in Functionalities)
        { 
            Register(FindObjectsOfType(kvp.Key));
        }
    }

    public void Register(Functionality f)
    { 
        foreach (var componentType in f.SubscribedComponents)
        {
            if (!Functionalities.ContainsKey(componentType)) 
            { 
                Functionalities.Add(componentType, new List<Functionality>());
            }

            Functionalities[componentType].Add(f);
        }
    }

    public void Register(params UnityEngine.Object[] objs)
    { 
        foreach (var obj in objs)
        { 
            if (!Objects.ContainsKey(obj.GetType()))
            {
                Objects.Add(obj.GetType(), new List<UnityEngine.Object>());
            }
            Objects[obj.GetType()].Add(obj);
        }
    }

    void Update()
    { 
        foreach (var kvp in Functionalities)
        { 
            foreach (var functionality in kvp.Value)
            { 
                functionality.Update(Objects[kvp.Key]);
            }
        }
    }
}