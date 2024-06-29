using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour, IService, IWindow
{
    [SerializeField] private TMP_Text _description;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        var globalCanvasGroup = ServiceLoacator.Instance.Get<GlobalCanvasGroup>();

        if (globalCanvasGroup != null)
        {
            globalCanvasGroup.CanvasGroup.DOFade(0.65f, 0.7f).SetUpdate(true);
        }

        _canvasGroup.DOFade(1, 0.7f).SetUpdate(true);

        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void Hide()
    {
        var canvasGroup = ServiceLoacator.Instance.Get<GlobalCanvasGroup>();

        if (canvasGroup != null)
        {
            canvasGroup.CanvasGroup.DOFade(0, 0.7f).SetUpdate(true);
        }

        _canvasGroup.DOFade(0, 0.7f).SetUpdate(true).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void NewGame()
    {
        SceneManager.LoadScene(Constants.SceneIndex.Game);
    }

    public void Exit()
    {
        SceneManager.LoadScene(Constants.SceneIndex.Menu);
    }

    public void SetDescription(string description)
    {
        _description.text = description;
    }
}
