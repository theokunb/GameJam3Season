public class AsteroidsMediator : Mediator<AsteroidsInteraction, AsteroidsViewCreator<AsteroidsViewInteraction>>
{
    private AsteroidsCreator<AsteroidsInteraction> _asteroidsCreator;
    private AsteroidsViewCreator<AsteroidsViewInteraction> _viewCreator;

    public AsteroidsMediator(AsteroidsCreator<AsteroidsInteraction> asteroids, AsteroidsViewCreator<AsteroidsViewInteraction> view)
    {
        _asteroidsCreator = asteroids;
        _viewCreator = view;
    }

    public override void Act()
    {
        _asteroidsCreator.Closing += OnClosing;
    }

    private void OnClosing()
    {
        _viewCreator.HideInteraction();
    }
}