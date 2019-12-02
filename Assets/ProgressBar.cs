using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    // Configuration
    public RectTransform Slider;
    public int MaxWidth;
    public int MaxWorkUnits;

    // Runtime
    float progress;

    public void SetProgress(float progress)
    {
        this.progress = progress;
        Slider.sizeDelta = new Vector2(Mathf.RoundToInt(MaxWidth * progress / MaxWorkUnits), Slider.sizeDelta.y);
    }
}
