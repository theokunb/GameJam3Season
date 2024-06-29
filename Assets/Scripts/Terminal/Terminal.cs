using System;
using UnityEngine;

public class Terminal : MonoBehaviour, IService, IInteractable
{
    [SerializeField] private InteractionStatusType _interactionStatus;
    [SerializeField] private QuestionType _question;

    private InteractionStatus _currentStatus;
    private TerminalEnterVisitor _enterVisitor;
    private TerminalExitVisitor _exitVisitor;
    private Animator _animator;

    public InteractionStatus InteractionStatus => _currentStatus;

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
            _currentStatus.Accept(_exitVisitor);

            ServiceLoacator.Instance.UnRegister<Terminal>();
        }
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _enterVisitor = new TerminalEnterVisitor(_animator);
        _exitVisitor = new TerminalExitVisitor(_animator);
        
        SetStatus(_interactionStatus);
    }

    public void SetStatus(InteractionStatusType status)
    {
        _currentStatus = status.ToInteractableStatus();
        SetQuestion(_question);
    }

    public void SetQuestion(QuestionType questionType, bool randomly = false)
    {
        if (randomly == false)
        {
            _enterVisitor.SetQuestion(questionType.ToQuestion());
        }
        else
        {
            var names = Enum.GetNames(typeof(QuestionType));
            var rand = UnityEngine.Random.Range(0, names.Length);
            _enterVisitor.SetQuestion(((QuestionType)rand).ToQuestion());
        }
    }

    public void Accept(IServiceVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class TerminalExitVisitor : IInteractionStatusVisitor
{
    private Animator _animator;

    public TerminalExitVisitor(Animator animator)
    {
        _animator = animator;
    }

    public void Visit(NotInteractable obj)
    {
        _animator?.SetBool(Constants.AnimationParams.Open, false);
        var interactionService = ServiceLoacator.Instance.Get<InteractionService>();

        interactionService.CancelInteraction();
    }

    public void Visit(Interactable obj)
    {
        _animator?.SetBool(Constants.AnimationParams.Open, false);

        var interactionService = ServiceLoacator.Instance.Get<InteractionService>();

        interactionService.CancelInteraction();
    }
}

public class TerminalEnterVisitor : IInteractionStatusVisitor
{
    private Question _question;
    private Animator _animator;
    IQuestionVisitor _visitor;

    public TerminalEnterVisitor(Animator animator)
    {
        _animator = animator;
        _visitor = new QuestionVisitor();
    }

    public void Visit(NotInteractable obj)
    {
        _animator?.SetBool(Constants.AnimationParams.Open, false);
    }

    public void Visit(Interactable obj)
    {
        _animator?.SetBool(Constants.AnimationParams.Open, true);

        var interactionService = ServiceLoacator.Instance.Get<InteractionService>();

        interactionService.ReqestInteraction(() =>
        {
            _question.Accept(_visitor);
        });
    }

    public void SetQuestion(Question question)
    {
        _question = question;
    }
}