using DG.Tweening;
using System;
using UnityEngine;

public abstract class Creator<T> : IService, IWindow where T : Component
{
    private Canvas _canvas;

    public event Action Closing;

    public Creator(Canvas canvas)
    {
        _canvas = canvas;
    }

    protected T CurrentInteraction;

    public T CreateInteraction(GameObject prefab)
    {
        var instance = UnityEngine.Object.Instantiate(prefab, _canvas.transform);
        instance.transform.SetSiblingIndex(1);

        instance.transform.position -= new Vector3(0, Screen.height, 0);
        float targetPostion = instance.transform.position.y + Screen.height;

        instance.transform.DOMoveY(targetPostion, 0.7f).OnComplete(() =>
        {
            AfterCreate();
        });

        var mainCanvasGroup = ServiceLoacator.Instance.Get<GlobalCanvasGroup>();
        mainCanvasGroup.CanvasGroup.alpha = 0;
        mainCanvasGroup.CanvasGroup.DOFade(0.65f, 0.7f);

        var player = ServiceLoacator.Instance.Get<Player>();
        player.enabled = false;

        CurrentInteraction = instance.GetComponent<T>();

        OnCreating();

        return instance.GetComponent<T>();
    }

    public void HideInteraction()
    {
        var player = ServiceLoacator.Instance.Get<Player>();
        player.enabled = true;
        Closing?.Invoke();

        var mainCanvasGroup = ServiceLoacator.Instance.Get<GlobalCanvasGroup>();
        mainCanvasGroup.CanvasGroup.alpha = 0.65f;
        mainCanvasGroup.CanvasGroup.DOFade(0f, 0.5f);
        OnHiding();
        CurrentInteraction?.transform.DOMoveY(-Screen.height, 0.7f).OnComplete(() =>
        {
            var interactionService = ServiceLoacator.Instance.Get<InteractionService>();
            interactionService.Free();

            UnityEngine.Object.Destroy(CurrentInteraction.gameObject);
        });
    }

    /// <summary>
    /// метод вызываетс€ во врем€ отображени€ мини игры
    /// </summary>
    protected abstract void OnCreating();


    /// <summary>
    /// метод вызываетс€ во врем€ исчезновени€ мини игры
    /// </summary>
    protected abstract void OnHiding();

    public virtual void AfterCreate()
    {
    }

    public void Hide()
    {
        HideInteraction();
    }
}
