using System.Collections;
using UnityEngine;

public class StartService : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private float _cameraSpeed;

    private GameTimer _gameTimer;
    private BadService _badService;
    private NavigationService _navigationService;
    private Player _player;

    private void Start()
    {
        _gameTimer = ServiceLoacator.Instance.Get<GameTimer>();
        _badService = ServiceLoacator.Instance.Get<BadService>();
        _navigationService = ServiceLoacator.Instance.Get<NavigationService>();
        _player = ServiceLoacator.Instance.Get<Player>();

        _gameTimer.enabled = false;
        _badService.enabled = false;
        _navigationService.enabled = false;
        _player.enabled = false;

        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        var camera = Camera.main;

        while(camera.transform.position != _cameraPosition.position)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, _cameraPosition.position, Time.deltaTime * _cameraSpeed);
            yield return null;
        }

        _gameTimer.enabled = true;
        _badService.enabled = true;
        _navigationService.enabled = true;
        _player.enabled = true;
    }
}
