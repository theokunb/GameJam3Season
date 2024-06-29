using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishService : MonoBehaviour, IService
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Camera _camera;
    [SerializeField] private TMP_Text _text;

    private GameTimer _gameTimer;
    private BadService _badService;
    private NavigationService _navigationService;
    private Player _player;
    private DialogService _dialogService;
    private GameProgress _gameProgress;

    public void RaiseEvent()
    {
        _gameTimer = ServiceLoacator.Instance.Get<GameTimer>();
        _badService = ServiceLoacator.Instance.Get<BadService>();
        _navigationService = ServiceLoacator.Instance.Get<NavigationService>();
        _player = ServiceLoacator.Instance.Get<Player>();
        _dialogService = ServiceLoacator.Instance.Get<DialogService>();
        _gameProgress = ServiceLoacator.Instance.Get<GameProgress>();

        _gameTimer.enabled = false;
        _badService.enabled = false;
        
        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        float timeout = 1.5f;
        float elapsedTime = 0;

        while(elapsedTime < timeout)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _navigationService.enabled = false;
        _player.enabled = false;

        _gameProgress.gameObject.SetActive(false);
        _gameTimer.gameObject.SetActive(false);

        _player.ResetVelocity();
        _player.SetAnimationParam(anim =>
        {
            anim.SetFloat(Constants.AnimationParams.Speed, 0);
        });

        StartDialog();
    }

    private void StartDialog()
    {
        var phrase1 = Resources.Load(Constants.Sounds.EndPhrase1) as AudioClip;
        var phrase2 = Resources.Load(Constants.Sounds.EndPhrase2) as AudioClip;
        var phrase3 = Resources.Load(Constants.Sounds.EndPhrase3) as AudioClip;

        _dialogService.CadetTalk(phrase1);
        _dialogService.CaptainTalk(phrase2);
        _dialogService.CadetTalk(phrase3);

        StartCoroutine(WaitDialog());
    }

    private IEnumerator WaitDialog()
    {
        while (_dialogService.IsPlaying)
        {
            yield return null;
        }
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.DOFade(1, 1f).SetUpdate(true).OnComplete(() =>
        {
            _camera.gameObject.SetActive(true);

            StartCoroutine(MoveText());
        });
    }

    private IEnumerator MoveText()
    {
        float elapsedTIme = 0;
        float timeout = 20;
        float textSpeed = 50;

        while (elapsedTIme < timeout)
        {
            _text.transform.position += _text.transform.up * textSpeed * Time.deltaTime;
            elapsedTIme += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(Constants.SceneIndex.Menu);
    }
}
