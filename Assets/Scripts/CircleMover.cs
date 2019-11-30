using System.Collections.Generic;
using UnityEngine;

public class CircleMover : MonoBehaviour
{
    // Configuration
    public AnimationCurve RadiusAnim;
    public AnimationCurve SpeedAnim;
    public AnimationCurve SizeAnim;
    public float AnimationLength;
    public float MovementSharpness;

    public delegate void AnimationComplete();
    public event AnimationComplete OnAnimationComplete;

    // Runtime
    bool animating;
    float animationProgress;
    List<Transform> transforms = new List<Transform>();
    float rotationOffset;

    public void AddTransform(Transform transform)
    {
        transform.SetParent(this.transform);
        transforms.Add(transform);
    }

    public void RemoveTransform(Transform transform)
    {
        transforms.Remove(transform);
    }

    public void ClearTransforms()
    {
        transforms.Clear();
    }

    public void SetVisible(bool visible)
    {
        if (!visible)
        {
            StopAnimation();
        }

        foreach (var transform in transforms)
        {
            transform.gameObject.SetActive(visible);
        }
    }

    public void StartAnimation()
    {
        animating = true;
    }

    public void StopAnimation()
    {
        animating = false;
        animationProgress = 0;
    }

    void Update()
    {
        float t = 0;
        if (animating)
        {
            animationProgress += Time.deltaTime;
            t = animationProgress;
        }

        rotationOffset += SpeedAnim.Evaluate(t) * Time.deltaTime;
        float radius = RadiusAnim.Evaluate(t);
        float scale = SizeAnim.Evaluate(t);

        for (int i = 0; i < transforms.Count; i++)
        {
            float theta = Mathf.PI * 2 / transforms.Count * i + rotationOffset;
            transforms[i].localScale = new Vector2(scale, scale);

            Vector2 targetPosition = new Vector2(Mathf.Cos(theta) * radius, Mathf.Sin(theta) * radius);
            transforms[i].localPosition = Vector2.Lerp(transforms[i].localPosition, targetPosition, 1 - Mathf.Exp(-MovementSharpness * Time.deltaTime));
        }

        if (animating && animationProgress >= AnimationLength)
        {
            OnAnimationComplete?.Invoke();
            animating = false;
            animationProgress = 0;
        }
    }
}