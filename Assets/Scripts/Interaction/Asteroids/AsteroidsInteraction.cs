using System;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidsInteraction : BaseInteraction
{
    [SerializeField] private Image _progress;
    [SerializeField] private Ship _ship;
    [SerializeField] private float _time;

    private float _elapsedTime;

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

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;
        _progress.fillAmount = _elapsedTime / _time;

        if(_elapsedTime > _time)
        {
            CompleteAction?.Invoke();
        }
    }

    private void OnDied()
    {
        OnLose?.Invoke(this);
    }

    public override void Accept(IInteractionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
