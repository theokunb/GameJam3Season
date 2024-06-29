using System;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidsInteraction : BaseInteraction
{
    [SerializeField] private Image _progress;
    [SerializeField] private Ship _ship;
    [SerializeField] private float _time;

    private float _elapsedTime;
    private AudioClip _success;
    private AudioClip _lose;
    private SoundContainer _soundContainer;

    public override event Action<BaseInteraction> OnLose;

    private void Awake()
    {
        _elapsedTime = 0;
    }

    private void OnEnable()
    {
        _ship.Died += OnDied;
    }

    private void OnDisable()
    {
        _ship.Died -= OnDied;
    }

    private void Start()
    {
        _soundContainer = ServiceLoacator.Instance.Get<SoundContainer>();
        _success = Resources.Load(Constants.Sounds.Success) as AudioClip;
        _lose = Resources.Load(Constants.Sounds.AsteroidsDie) as AudioClip;
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;
        _progress.fillAmount = _elapsedTime / _time;

        if(_elapsedTime > _time)
        {
            _soundContainer.Play(_success, conf =>
            {
                conf.loop = false;
                conf.volume = 0.1f;
            });
            CompleteAction?.Invoke();
            enabled = false;
        }
    }

    private void OnDied()
    {
        _soundContainer.Play(_lose, conf =>
        {
            conf.loop = false;
            conf.volume = 0.1f;
        });
        OnLose?.Invoke(this);
    }

    public override void Accept(IInteractionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
