using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GlobalCanvasGroup : OrderedMonobeh, IService
{
    private CanvasGroup _canvasGroup;

    public CanvasGroup CanvasGroup => _canvasGroup;

    public override void OrderedAwake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
}
