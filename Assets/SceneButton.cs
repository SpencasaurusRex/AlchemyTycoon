using UnityEngine;
using UnityEngine.EventSystems;

public class SceneButton : MonoBehaviour, IPointerClickHandler
{
    [Header("Configuration")]
    public int TargetSceneIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.dragging) return;
        GameController.Instance.SwitchScene(TargetSceneIndex);
    }
}
