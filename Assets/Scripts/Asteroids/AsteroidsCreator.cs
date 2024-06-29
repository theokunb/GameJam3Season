using UnityEngine;

public class AsteroidsCreator<T> : Creator<T> where T : BaseInteraction
{
    public AsteroidsCreator(Canvas canvas) : base(canvas)
    {
    }

    protected override void OnCreating()
    {
        CurrentInteraction.OnLose += OnLose;

        CurrentInteraction.OnComplete(() =>
        {
            var asteroids = ServiceLoacator.Instance.Get<Asteroids>();
            var score = ServiceLoacator.Instance.Get<Score>();
            var interactionCompleteVisitor = ServiceLoacator.Instance.Get<InteractionCompleteVisitor>();
            var windowController = ServiceLoacator.Instance.Get<WindowController>();

            windowController.Pop();

            CurrentInteraction.Accept(interactionCompleteVisitor);

            asteroids.SetStatus(InteractionStatusType.NotInteractable);
            CurrentInteraction.enabled = false;
            CurrentInteraction.Accept(score);
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
