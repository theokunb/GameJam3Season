using DG.Tweening;
using System;
using UnityEngine;

public class TerminalCreator<T> : Creator<T> where T : BaseInteraction
{
    private Action<T> _action;

    public TerminalCreator(Canvas canvas) : base(canvas)
    {
    }

    protected override void OnCreating()
    {
        CurrentInteraction.OnComplete(() =>
        {
            var terminal = ServiceLoacator.Instance.Get<Terminal>();
            var score = ServiceLoacator.Instance.Get<Score>();
            var interactionCompleteVisitor = ServiceLoacator.Instance.Get<InteractionCompleteVisitor>();
            var windowController = ServiceLoacator.Instance.Get<WindowController>();

            windowController.Pop();

            terminal.SetStatus(InteractionStatusType.NotInteractable);
            CurrentInteraction.Accept(score);
            CurrentInteraction.Accept(interactionCompleteVisitor);
        });
    }
    protected override void OnHiding()
    {
    }

    public void SetQuestion(Action<T> action)
    {
        _action = action;
    }

    public override void AfterCreate()
    {
        _action?.Invoke(CurrentInteraction);
    }
}
