using UnityEngine;

public class Door : MonoBehaviour, IService, IInteractable
{
    [SerializeField] private InteractionStatusType _doorStatus;
    [SerializeField] private UnlockDifficultType _unlockDifficult;

    private DoorIndicator _indicator;
    private DoorEnterVisitor _doorEnterVisitor;
    private DoorExitVisitor _doorExitVisitor;
    private Animator _animator;

    public InteractionStatus InteractionStatus => _indicator.CurrentDoorStatus;

    private void Awake()
    {
         _indicator = GetComponentInChildren<DoorIndicator>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _doorEnterVisitor = new DoorEnterVisitor(_animator, _unlockDifficult.ToUnlockDifficult());
        _doorExitVisitor = new DoorExitVisitor(_animator);

        SetStatus(_doorStatus);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            _indicator.CurrentDoorStatus?.Accept(_doorEnterVisitor);

            ServiceLoacator.Instance.Register(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            _indicator.CurrentDoorStatus?.Accept(_doorExitVisitor);

            ServiceLoacator.Instance.UnRegister<Door>();
        }
    }

    public void SetStatus(InteractionStatusType doorStatus)
    {
        _indicator.SetStatus(doorStatus.ToInteractableStatus(), _unlockDifficult);
    }

    public void Accept(IServiceVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void PlaySound()
    {
        var audioClip = Resources.Load(Constants.Sounds.DoopOpen) as AudioClip;
        var soundContainer = ServiceLoacator.Instance.Get<SoundContainer>();

        soundContainer.Play(audioClip, conf =>
        {
            conf.loop = false;
            conf.volume = 0.1f;
        });
    }
}

public class DoorExitVisitor : IInteractionStatusVisitor
{
    private readonly Animator _animator;

    public DoorExitVisitor(Animator animator)
    {
        _animator = animator;
    }

    public void Visit(NotInteractable obj)
    {
        _animator.SetBool(Constants.AnimationParams.Open, false);
    }

    public void Visit(Interactable obj)
    {
        var interactionService = ServiceLoacator.Instance.Get<InteractionService>();
        
        _animator.SetBool(Constants.AnimationParams.Open, false);
        interactionService.CancelInteraction();
    }
}

public class DoorEnterVisitor : IInteractionStatusVisitor
{
    private readonly Animator _animator;
    private readonly UnlockDifficult _unlockDifficult;
    private readonly UnlockDifficultVisitor<DoorInteraction> _unlockVisitor;

    public DoorEnterVisitor(Animator animator, UnlockDifficult unlockDifficult)
    {
        _animator = animator;
        _unlockDifficult = unlockDifficult;
        var mainCanvas = ServiceLoacator.Instance.Get<GlobalCanvas>();

        _unlockVisitor = new UnlockDifficultVisitor<DoorInteraction>(mainCanvas.Canvas);
    }

    public void Visit(NotInteractable obj)
    {
        _animator.SetBool(Constants.AnimationParams.Open, true);
    }

    public void Visit(Interactable obj)
    {
        var isOpen = _animator.GetBool(Constants.AnimationParams.Open);

        if(isOpen)
        {
            return;
        }

        var interactionService = ServiceLoacator.Instance.Get<InteractionService>();
        var windows = ServiceLoacator.Instance.Get<WindowController>();

        interactionService.ReqestInteraction(() =>
        {
            _unlockDifficult.Accept(_unlockVisitor);
            windows.Push(_unlockVisitor);
        });
    }
}