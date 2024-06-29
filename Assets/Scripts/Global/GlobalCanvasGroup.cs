using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GlobalCanvasGroup : MonoBehaviour, IService
{
    private CanvasGroup _canvasGroup;

    public CanvasGroup CanvasGroup => _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
}
