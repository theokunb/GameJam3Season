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
        if (value == _currentValue)
        {
            _currentValue++;

            button.SetColor(Constants.TerminalButton.AcceptedColor);
        }
        else
        {
            _currentValue = 1;

            foreach (var buttom in _buttons)
            {
                buttom.SetColor(Constants.TerminalButton.DefaultColor);
            }
        }

        if (_currentValue == _buttons.Length + 1)
        {
            CompleteAction?.Invoke();
        }
    }

    public override void Accept(IInteractionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
