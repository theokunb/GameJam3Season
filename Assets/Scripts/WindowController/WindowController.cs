using System.Collections.Generic;

public class WindowController : IService
{
    private Stack<IWindow> _windows;
    private NewInput _intup;

    public WindowController()
    {
        _windows = new Stack<IWindow>();
        _intup = new NewInput();
        _intup.Enable();
        _intup.Player.Escape.performed += OnEscape;
    }

    private void OnEscape(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(_windows.TryPop(out IWindow window))
        {
            window.Hide();
        }
        else
        {
            var gameMenu = ServiceLoacator.Instance.Get<GameMenu>();
            gameMenu.gameObject.SetActive(true);
            _windows.Push(gameMenu);
        }
        
    }

    public void Push(IWindow window)
    {
        _windows.Push(window);
    }


}
public interface IWindow
{
    void Hide();
}