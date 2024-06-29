using UnityEngine;

public class TextureRendererCanvas : MonoBehaviour, IService
{
    private Canvas _canvas;

    public Canvas Canvas => _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }
}
