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
            
            tube.SetStatus(InteractionStatusType.NotInteractable);
            CurrentInteraction.Accept(score);
            CurrentInteraction.Accept(interactionCompleteVisitor);

            HideInteraction();
        });
    }

    protected override void OnHiding()
    {
    }
}
