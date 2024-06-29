using UnityEngine;

public class TubeCreator<T> : Creator<T> where T : BaseInteraction
{
    public TubeCreator(Canvas canvas) : base(canvas)
    {
    }

    protected override void OnCreating()
    {
        CurrentInteraction.OnComplete(() =>
        {
            var tube = ServiceLoacator.Instance.Get<Tube>();
            var score = ServiceLoacator.Instance.Get<Score>();
            var interactionCompleteVisitor = ServiceLoacator.Instance.Get<InteractionCompleteVisitor>();
            var windowController = ServiceLoacator.Instance.Get<WindowController>();

            windowController.Pop();

            tube.SetStatus(InteractionStatusType.NotInteractable);
            CurrentInteraction.Accept(score);
            CurrentInteraction.Accept(interactionCompleteVisitor);
        });
    }

    protected override void OnHiding()
    {
    }
}
