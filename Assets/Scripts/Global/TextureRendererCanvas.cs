using UnityEngine;

public class TextureRendererCanvas : OrderedMonobeh, IService
{
    private Canvas _canvas;

    public Canvas Canvas => _canvas;

    public override void OrderedAwake()
    {
        _canvas = GetComponent<Canvas>();
    }
}
