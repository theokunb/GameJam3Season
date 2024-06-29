using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class GlobalCanvas : MonoBehaviour, IService
{
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    public Canvas Canvas => _canvas;
}
