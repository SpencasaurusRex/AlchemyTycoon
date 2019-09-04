using Sirenix.OdinInspector;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    SpriteRenderer sr;
    Canvas cv;
    Camera cam;
    AudioListener aud;

    [ShowInInspector]
    public bool Invisible { get; set; }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cv = GetComponent<Canvas>();
        cam = GetComponent<Camera>();
        aud = GetComponent<AudioListener>();

        GameController.Instance.Register(this);
    }

    public void Show()
    {
        if (sr != null) sr.enabled = !Invisible;
        if (cv != null) cv.enabled = !Invisible;
        if (cam != null) cam.enabled = true;
        if (aud != null) aud.enabled = true;
    }

    public void Hide(bool switchingScene = false)
    {
        if (switchingScene)
        {
            var s = false;
            var c = false;
            if (sr != null) s = sr.enabled;
            if (cv != null) c = cv.enabled;

            Invisible = !(s || c);
        }

        if (sr != null) sr.enabled = false;
        if (cv != null) cv.enabled = false;
        if (cam != null) cam.enabled = false;
        if (aud != null) aud.enabled = false;
    }
}