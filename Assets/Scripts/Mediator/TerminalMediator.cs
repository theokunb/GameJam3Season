public class TerminalMediator : Mediator<TerminalViewCreator<TerminalViewInteraction>, TerminalInteraction>
{
    private readonly TerminalViewCreator<TerminalViewInteraction> _viewCreator;
    private readonly TerminalCreator<TerminalInteraction> _interactionCreator;

    public TerminalMediator(TerminalViewCreator<TerminalViewInteraction> viewCreator, TerminalCreator<TerminalInteraction> interaction)
    {
        _viewCreator = viewCreator;
        _interactionCreator = interaction;
    }

    public override void Act()
    {
        _interactionCreator.Closing += OnClosing;
    }

    private void OnClosing()
    {
        _viewCreator.HideInteraction();
    }
}
