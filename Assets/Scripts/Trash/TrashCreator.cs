using UnityEngine;

public class TrashCreator<T> : Creator<T> where T : BaseInteraction
{
    public TrashCreator(Canvas canvas) : base(canvas)
    {
    }

    protected override void OnCreating()
    {
        CurrentInteraction.OnComplete(() =>
        {
            var trash = ServiceLoacator.Instance.Get<Trash>();
            var score = ServiceLoacator.Instance.Get<Score>();
            var interactionCompleteVisitor = ServiceLoacator.Instance.Get<InteractionCompleteVisitor>();
            
            trash.SetStatus(InteractionStatusType.NotInteractable);
            CurrentInteraction.Accept(score);
            CurrentInteraction.Accept(interactionCompleteVisitor);

            HideInteraction();
        });
    }

    protected override void OnHiding()
    {
    }
    private void OnEscape(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        HideInteraction();
    }
}