using UnityEngine;

public class Drill : MonoBehaviour, IService, IInteractable
{
    [SerializeField] private InteractionStatusType _status;

    private InteractionStatus _interactionStatus;
    private IInteractionStatusVisitor _visitor;

    public InteractionStatus InteractionStatus => _interactionStatus;

    private void Start()
    {
        _visitor = new DrillStatusVisitor();

        SetStatus(_status);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            ServiceLoacator.Instance.Register(this);

            _interactionStatus.Accept(_visitor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            ServiceLoacator.Instance.UnRegister<Drill>();

            var interactionService = ServiceLoacator.Instance.Get<InteractionService>();
            interactionService.CancelInteraction();
        }
    }

    public void SetStatus(InteractionStatusType status)
    {
        _interactionStatus = status.ToInteractableStatus();
    }

    public void Accept(IServiceVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class DrillStatusVisitor : IInteractionStatusVisitor
{
    private DrillCreator<DrillInteraction> _creator;

    public DrillStatusVisitor()
    {
        var mainCanvas = ServiceLoacator.Instance.Get<GlobalCanvas>();
        _creator = new DrillCreator<DrillInteraction>(mainCanvas.Canvas);
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
            var prefab = Resources.Load(Constants.Interactions.Drill) as GameObject;

            windows.Push(_creator);
            _creator.CreateInteraction(prefab);
        });
    }
}
