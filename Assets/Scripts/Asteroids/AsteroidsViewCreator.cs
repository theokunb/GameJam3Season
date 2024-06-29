using UnityEngine;

public class AsteroidsViewCreator<T> : Creator<T> where T : BaseInteraction
{
    private NewInput _input;

    public AsteroidsViewCreator(Canvas canvas) : base(canvas)
    {
        _input = new NewInput();
        _input.Enable();
    }
    protected override void OnCreating()
    {
        _input.Player.Escape.performed += OnEscape;
        CurrentInteraction.OnLose += OnLose;
    }

    protected override void OnHiding()
    {
        _input.Player.Escape.performed -= OnEscape;
        CurrentInteraction.OnLose -= OnLose;
    }

    private void OnEscape(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        HideInteraction();
    }

    private void OnLose(BaseInteraction obj)
    {
        HideInteraction();
    }
}
