using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))]
public class Tool : SerializedMonoBehaviour, IDragReceiver
{
    const int WAITING = 0;
    const int PROCESSING = 1;

    // Configuration
    public List<PhysicalState> AcceptedPhysical;
    public float ProcessTime = 20;
    public List<AttributeAffector> Affectors;
    public PhysicalState[] PhysicalConversions;
    public Color MultiplyColor;

    // Runtime
    int currentState;
    float currentProcessingProgress;
    IngredientMix processingTarget;
    SpriteRenderer processingRender;
    ProgressBar progressBar;

    void Awake()
    {
        progressBar = GetComponentInChildren<ProgressBar>();
        progressBar.gameObject.SetActive(false);
        progressBar.MaxWorkUnits = (int)ProcessTime;
    }

    void Start()
    {
        var interactable = GetComponent<Interactable>();
        interactable.Register(this);
        interactable.OnReceive += Receive;
    }

    void Update()
    {
        if (currentState == PROCESSING)
        {
            currentProcessingProgress += Time.deltaTime;
            progressBar.SetProgress(currentProcessingProgress);

            if (currentProcessingProgress >= ProcessTime)
            {
                FinishProcessing();
            }
        }
    }

    void StartProcessing(IngredientMix ing, SpriteRenderer renderer)
    {
        processingTarget = ing;
        processingRender = renderer;

        renderer.enabled = false;
        currentState = PROCESSING;

        progressBar.gameObject.SetActive(true);
    }

    void FinishProcessing()
    {
        foreach (var affector in Affectors)
        {
            processingTarget.Modify(affector);
        }

        if (PhysicalConversions.Length > (int)processingTarget.PhysicalState)
        {
            PhysicalState conversion = PhysicalConversions[(int)processingTarget.PhysicalState];
            if (conversion > 0)
            {
                // TODO change sprite based on this
                processingTarget.PhysicalState = conversion;
            }
        }

        progressBar.gameObject.SetActive(false);

        processingRender.enabled = true;
        processingTarget.transform.position = transform.position + new Vector3(1, 0);
        processingRender.color *= MultiplyColor;

        currentProcessingProgress = 0;
        currentState = WAITING;
    }

    public bool CanReceive(GameObject obj)
    {
        if (currentState == PROCESSING) return false;

        var ingredientMix = obj.GetComponent<IngredientMix>();
        if (ingredientMix == null) return false;

        var renderer = obj.GetComponent<SpriteRenderer>();
        if (!AcceptedPhysical.Contains(ingredientMix.PhysicalState)) return false;

        return true;
    }

    public void Receive(GameObject obj)
    {
        var ingredientMix = obj.GetComponent<IngredientMix>();
        var renderer = obj.GetComponent<SpriteRenderer>();

        StartProcessing(ingredientMix, renderer);
    }
}

[Serializable]
public class AttributeAffector
{
    public int Index;
    public int Delta;
}