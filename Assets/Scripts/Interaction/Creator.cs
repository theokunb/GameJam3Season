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

        EnterMoveAnimation(instance.transform);
        EnterFadeAnimation();

        var player = ServiceLoacator.Instance.Get<Player>();
        player.enabled = false;

        CurrentInteraction = instance.GetComponent<T>();

        OnCreating();

        return instance.GetComponent<T>();
    }

    protected virtual void EnterMoveAnimation(Transform transform)
    {
        transform.position -= new Vector3(0,Screen.height,0);
        float targetPostion = transform.position.y + Screen.height;

        transform.DOMoveY(targetPostion, 0.7f).OnComplete(() =>
        {
            AfterCreate();
        });
    }

    protected virtual void EnterFadeAnimation()
    {
        var mainCanvasGroup = ServiceLoacator.Instance.Get<GlobalCanvasGroup>();
        mainCanvasGroup.CanvasGroup.alpha = 0;
        mainCanvasGroup.CanvasGroup.DOFade(0.65f, 0.7f);
    }

    public void HideInteraction()
    {
        var player = ServiceLoacator.Instance.Get<Player>();
        player.enabled = true;
        Closing?.Invoke();

        OutFadeAnimation();
        OnHiding();
        OutMoveAnimation();
    }

    protected virtual void OutFadeAnimation()
    {
        var mainCanvasGroup = ServiceLoacator.Instance.Get<GlobalCanvasGroup>();
        mainCanvasGroup.CanvasGroup.alpha = 0.65f;
        mainCanvasGroup.CanvasGroup.DOFade(0f, 0.5f);
    }

    /// <summary>
    /// если вы переопределяете данный метод обязательно вызовите метод DestroyInteraction()
    /// </summary>
    protected virtual void OutMoveAnimation()
    {
        CurrentInteraction?.transform.DOMoveY(-Screen.height, 0.7f).OnComplete(() =>
        {
            DestroyInteraction();
        });
    }

    protected void DestroyInteraction()
    {
        var interactionService = ServiceLoacator.Instance.Get<InteractionService>();
        interactionService.Free();

        UnityEngine.Object.Destroy(CurrentInteraction.gameObject);
    }

    /// <summary>
    /// метод вызывается во время отображения мини игры
    /// </summary>
    protected abstract void OnCreating();


    /// <summary>
    /// метод вызывается во время исчезновения мини игры
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
