using UnityEngine;

public class Asteroids : MonoBehaviour, IService, IInteractable
{
    [SerializeField] private InteractionStatusType _status;

    private InteractionStatus _currentStatus;
    private IInteractionStatusVisitor _statusVisitor;

    public InteractionStatus InteractionStatus => _currentStatus;

    private void Start()
    {
        SetStatus(_status);

        _statusVisitor = new AsteroidsStatusVisitor();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            ServiceLoacator.Instance.Register(this);

            _currentStatus.Accept(_statusVisitor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            ServiceLoacator.Instance.UnRegister<Asteroids>();

            var interactionService = ServiceLoacator.Instance.Get<InteractionService>();
            interactionService.CancelInteraction();
        }
    }

    public void SetStatus(InteractionStatusType status)
    {
        _currentStatus = status.ToInteractableStatus();
    }

    public void Accept(IServiceVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class AsteroidsStatusVisitor : IInteractionStatusVisitor
{
    private AsteroidsCreator<AsteroidsInteraction> _asteroidsCreator;
    private AsteroidsViewCreator<AsteroidsViewInteraction> _viewCreator;
    private AsteroidsMediator _mediator;

    public AsteroidsStatusVisitor()
    {
        var mainCanvas = ServiceLoacator.Instance.Get<GlobalCanvas>();
        var textureCanvas = ServiceLoacator.Instance.Get<TextureRendererCanvas>();

        _asteroidsCreator = new AsteroidsCreator<AsteroidsInteraction>(textureCanvas.Canvas);
        _viewCreator = new AsteroidsViewCreator<AsteroidsViewInteraction>(mainCanvas.Canvas);
    }

    public void Visit(NotInteractable obj)
    {
        
    }

    public void Visit(Interactable obj)
    {
        var interactionService = ServiceLoacator.Instance.Get<InteractionService>();
        var windows = ServiceLoacator.Instance.Get<WindowController>();

        interactionService.ReqestInteraction(() =>
        {
            var prefab = Resources.Load(Constants.Interactions.Asteroids) as GameObject;
            var viewPrefab = Resources.Load(Constants.Interactions.AsteroidsView) as GameObject;

            _asteroidsCreator.CreateInteraction(prefab);
            _viewCreator.CreateInteraction(viewPrefab);

            windows.Push(_asteroidsCreator);

            _mediator = new AsteroidsMediator(_asteroidsCreator, _viewCreator);
            _mediator.Act();
        });
    }
}
