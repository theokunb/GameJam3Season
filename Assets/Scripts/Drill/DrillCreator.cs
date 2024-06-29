using UnityEngine;

public class DrillCreator<T> : Creator<T> where T : BaseInteraction
{
    public DrillCreator(Canvas canvas) : base(canvas)
    {
    }

    protected override void OnCreating()
    {
        CurrentInteraction.OnLose += OnLose;

        CurrentInteraction.OnComplete(() =>
        {
            var drill = ServiceLoacator.Instance.Get<Drill>();
            var score = ServiceLoacator.Instance.Get<Score>();
            var interactionCompleteVisitor = ServiceLoacator.Instance.Get<InteractionCompleteVisitor>();

            drill.SetStatus(InteractionStatusType.NotInteractable);
            CurrentInteraction.Accept(score);
            CurrentInteraction.Accept(interactionCompleteVisitor);

            HideInteraction();
        });
    }

    protected override void OnHiding()
    {
        CurrentInteraction.OnLose -= OnLose;
    }

    private void OnLose(BaseInteraction interaction)
    {
        HideInteraction();
    }
}
