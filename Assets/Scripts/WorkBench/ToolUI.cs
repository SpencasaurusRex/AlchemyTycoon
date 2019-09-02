using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{
    [Header("Configuration")]
    [Required]
    public Image ProgressBarOuter;
    [Required]
    public Image ProgressBarInner;
    [Required]
    public Image Arrow;

    void Start()
    {
        Processing(false);
    }

    public void Processing(bool processing)
    {
        if (GameController.CurrentScene(gameObject))
        {
            ProgressBarOuter.enabled = processing;
            ProgressBarInner.enabled = processing;
        }
    }
}
