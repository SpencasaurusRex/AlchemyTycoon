using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Header("Configuration")]
    public Image ScaledImage;

    void Start()
    {
        SetPercent(0);
    }

    public void SetPercent(float percent)
    {
        ScaledImage.rectTransform.localScale = new Vector3(percent, 1, 1);
    }
}
