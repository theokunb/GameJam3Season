using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class GlobalCanvas : OrderedMonobeh, IService
{
    private Canvas _canvas;

    public override void OrderedAwake()
    {
        _canvas = GetComponent<Canvas>();
    }

    public Canvas Canvas => _canvas;
}
