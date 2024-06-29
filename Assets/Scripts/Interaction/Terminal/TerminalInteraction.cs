using DG.Tweening;
using TMPro;
using UnityEngine;

public class TerminalInteraction : BaseInteraction
{
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private TMP_Text _arrowText;
    [SerializeField] private TMP_InputField _answerText;

    private NewInput _input;
    private string _answer;
    private AudioClip _wrong;
    private AudioClip _success;
    private SoundContainer _soundContainer;

    private void Awake()
    {
        _input = new NewInput();
        _arrowText.gameObject.SetActive(false);
        _answerText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Enter.performed += OnEnter;
        _input.Player.Click.performed += OnClick;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Enter.performed -= OnEnter;
    }

    private void Start()
    {
        _soundContainer = ServiceLoacator.Instance.Get<SoundContainer>();
        _success = Resources.Load(Constants.Sounds.Success) as AudioClip;
        _wrong = Resources.Load(Constants.Sounds.WrongClick) as AudioClip;
    }

    private void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _answerText.ActivateInputField();
    }

    private void OnEnter(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(_answerText.text.ToLower() == _answer)
        {
            _soundContainer.Play(_success, conf =>
            {
                conf.loop = false;
                conf.volume = 0.1f;
            });
            CompleteAction?.Invoke();
        }
        else
        {
            _soundContainer.Play(_wrong, conf =>
            {
                conf.loop = false;
                conf.volume = 0.1f;
            });
            _answerText.text = string.Empty;
        }

        _answerText.ActivateInputField();
    }

    public void SetQuestion(QuestionDto questionDto)
    {
        _answer = questionDto.Answer.ToLower();

        _questionText.DOText(questionDto.Question, 5f).OnComplete(() =>
        {
            _answerText.gameObject.SetActive(true);
            _answerText.ActivateInputField();
            _arrowText.gameObject.SetActive(true);
        });
    }

    public override void Accept(IInteractionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
