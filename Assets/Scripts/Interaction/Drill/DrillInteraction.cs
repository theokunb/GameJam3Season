using System;
using UnityEngine;
using UnityEngine.UI;

public class DrillInteraction : BaseInteraction
{
    [SerializeField] private RectTransform _arrowTransform;
    [SerializeField] private Image _timer;
    [SerializeField] private float _roationSpeed;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private float _time;

    private Animator _animator;
    private NewInput _input;
    private float _currentAngle;
    private int _direction;
    private float _elapsedTime;
    private SoundContainer _soundContainer;
    private AudioClip _wrong;
    private AudioClip _success;

    public override event Action<BaseInteraction> OnLose;

    private void Awake()
    {
        _input = new NewInput();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _direction = 1;
        _currentAngle = 0;
        _elapsedTime = 0;
        _soundContainer = ServiceLoacator.Instance.Get<SoundContainer>();
        _wrong = Resources.Load(Constants.Sounds.WrongClick) as AudioClip;
        _success = Resources.Load(Constants.Sounds.Success) as AudioClip;
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Click.performed += OnClick;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Click.performed -= OnClick;
    }

    private void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(Mathf.Abs(_currentAngle) < 10)
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
            _animator.SetTrigger(Constants.AnimationParams.Wrong);
            _soundContainer.Play(_wrong, conf =>
            {
                conf.loop = false;
                conf.volume = 0.1f;
            });
        }
    }

    public void Update()
    {
        RotateArrow();

        _elapsedTime += Time.deltaTime;

        var totalTime = _time - _elapsedTime;

        if(totalTime <= 0)
        {
            OnLose?.Invoke(this);
        }

        _timer.fillAmount = totalTime / _time;
    }

    public void RotateArrow()
    {
        _currentAngle += Time.deltaTime * _roationSpeed * _direction;

        _arrowTransform.transform.eulerAngles = new Vector3(0, 0, _currentAngle);

        if (_currentAngle < minAngle || _currentAngle > maxAngle)
        {
            _direction *= -1;
        }
    }
    public override void Accept(IInteractionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
