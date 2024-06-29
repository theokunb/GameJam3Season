using UnityEngine;

public class Tube : MonoBehaviour, IService, IInteractable
{
    [SerializeField] private InteractionStatusType _interactionStatus;

    private InteractionStatus _currentStatus;
    private TubeEnterVisitor _enterVisitor;

    public InteractionStatus InteractionStatus => _currentStatus;

    private void Start()
    {
        SetStatus(_interactionStatus);
        _enterVisitor = new TubeEnterVisitor();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            _currentStatus.Accept(_enterVisitor);

            ServiceLoacator.Instance.Register(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            var interactionService = ServiceLoacator.Instance.Get<InteractionService>();
            interactionService.CancelInteraction();

            ServiceLoacator.Instance.UnRegister<Tube>();
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

public class TubeEnterVisitor : IInteractionStatusVisitor
{
    private TubeCreator<TubeInteraction> _creator;

    public TubeEnterVisitor()
    {
        var mainCanvas = ServiceLoacator.Instance.Get<GlobalCanvas>();

        _creator = new TubeCreator<TubeInteraction>(mainCanvas.Canvas);
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
            var prefab = Resources.Load(Constants.Interactions.Tube) as GameObject;

            windows.Push(_creator);
            _creator.CreateInteraction(prefab);
        });
    }
}