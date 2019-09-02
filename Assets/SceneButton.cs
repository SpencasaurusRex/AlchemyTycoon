using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour, IPointerClickHandler
{
    [Header("Configuration")]
    public Scene TargetScene;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.dragging) return;
        GameController.Instance.SwitchScene(TargetScene);
    }
}
