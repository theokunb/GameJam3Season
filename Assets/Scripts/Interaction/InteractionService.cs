using System;

public class InteractionService : IService
{
    private NewInput _input;
    private Action _action;
    private bool _interactionRequested;
    private bool _isBusy;
    private GameUI _gameUI;

    private GameUI GameUI
    {
        get => _gameUI ?? ServiceLoacator.Instance.Get<GameUI>();
    }

    public InteractionService()
    {
        _input = new NewInput();
        _interactionRequested = false;
        _isBusy = false;

        _input.Enable();
    }

    public void ReqestInteraction(Action action)
    {
        if (_interactionRequested == true)
        {
            return;
        }

        GameUI?.ShowInteractButton();

        _input.Player.Interact.performed += OnInteract;

        _action = action;

        _interactionRequested = true;
    }

    public void CancelInteraction()
    {
        GameUI?.HideInteractButton();

        _input.Player.Interact.performed -= OnInteract;

        _interactionRequested = false;
    }

    private void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_isBusy == true)
        {
            return;
        }

        GameUI?.HideInteractButton();
        _isBusy = true;
        _action?.Invoke();
    }

    public void Free()
    {
        _isBusy = false;

        CancelInteraction();
    }
}