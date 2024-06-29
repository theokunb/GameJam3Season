using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TerminalButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Image _image;

    public event Action<int, TerminalButton> Clicked;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetValue(int value)
    {
        _text.text = value.ToString();
    }

    public void OnClick()
    {
        if(int.TryParse(_text.text, out int value))
        {
            Clicked?.Invoke(value, this);
        }
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }
}
