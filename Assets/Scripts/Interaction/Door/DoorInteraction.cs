using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : BaseInteraction
{
    [SerializeField] private TerminalButton[] _buttons;

    private int _currentValue = 1;

    private void OnEnable()
    {
        var buttonsCount = _buttons.Length;
        var values = new List<int>();
        _currentValue = 1;

        for (int i = 0; i < buttonsCount; i++)
        {
            values.Add(i + 1);
        }

        for (int i = 0; i < buttonsCount; i++)
        {
            var randomIndex = UnityEngine.Random.Range(0, buttonsCount);

            (values[i], values[randomIndex]) = (values[randomIndex], values[i]);
        }

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetValue(values[i]);

            _buttons[i].Clicked += OnButtonClicked;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].Clicked -= OnButtonClicked;
        }
    }

    private void OnButtonClicked(int value, TerminalButton button)
    {
        var soundContainer = ServiceLoacator.Instance.Get<SoundContainer>();
        var goodClick = Resources.Load(Constants.Sounds.ButtonClick) as AudioClip;
        var wrongClick = Resources.Load(Constants.Sounds.WrongClick) as AudioClip;
        var success = Resources.Load(Constants.Sounds.Success) as AudioClip;

        if (value == _currentValue)
        {
            _currentValue++;

            button.SetColor(Constants.TerminalButton.AcceptedColor);
            soundContainer.Play(goodClick, conf =>
            {
                conf.loop = false;
                conf.volume = 0.15f;
            });
        }
        else
        {
            _currentValue = 1;
            soundContainer.Play(wrongClick, conf =>
            {
                conf.loop = false;
                conf.volume = 0.1f;
            });

            foreach (var buttom in _buttons)
            {
                buttom.SetColor(Constants.TerminalButton.DefaultColor);
            }
        }

        if (_currentValue == _buttons.Length + 1)
        {
            soundContainer.Play(success, conf =>
            {
                conf.loop = false;
                conf.volume = 0.1f;
            });
            CompleteAction?.Invoke();
        }
    }

    public override void Accept(IInteractionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
