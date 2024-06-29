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

    private void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _answerText.ActivateInputField();
    }

    private void OnEnter(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(_answerText.text.ToLower() == _answer)
        {
            CompleteAction?.Invoke();
        }
        else
        {
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
