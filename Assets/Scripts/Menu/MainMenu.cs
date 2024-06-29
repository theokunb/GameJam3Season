using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(Constants.SceneIndex.Game);
    }

    public void Exit()
    {
        Application.Quit(0);
    }
}
