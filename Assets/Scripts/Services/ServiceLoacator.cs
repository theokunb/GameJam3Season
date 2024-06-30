using System.Collections.Generic;
using UnityEngine;

public class ServiceLoacator : OrderedMonobeh
{
    [SerializeField] private GlobalCanvasGroup _mainCanvasGroup;
    [SerializeField] private GlobalCanvas _mainCanvas;
    [SerializeField] private TextureRendererCanvas _rendererCanvas;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private DialogService _dialogService;
    [SerializeField] private Player _player;
    [SerializeField] private BadService _badService;
    [SerializeField] private GameMenu _gameMenu;
    [SerializeField] private FinishService _finishService;
    [SerializeField] private SoundContainer _soundContainer;
    [SerializeField] private GameTimer _gameTimer;
    [SerializeField] private NavigationService _navigationService;
    [SerializeField] private GameProgress _gameProgress;

    public static ServiceLoacator Instance { get; private set; }

    private Dictionary<string, IService> _services;

    public override void OrderedAwake()
    {
        Instance = this;
        _services = new Dictionary<string, IService>();

        var materialFactory = new MaterialFactory();
        var interactionService = new InteractionService();
        var score = new Score();
        var interactionCompleteVisitor = new InteractionCompleteVisitor();
        var windowController = new WindowController();

        Instance.Register(materialFactory);
        Instance.Register(interactionService);
        Instance.Register(_mainCanvasGroup);
        Instance.Register(_mainCanvas);
        Instance.Register(_rendererCanvas);
        Instance.Register(_gameUI);
        Instance.Register(score);
        Instance.Register(_dialogService);
        Instance.Register(interactionCompleteVisitor);
        Instance.Register(_player);
        Instance.Register(windowController);
        Instance.Register(_gameMenu);
        Instance.Register(_finishService);
        Instance.Register(_soundContainer);
        Instance.Register(_badService);
        Instance.Register(_gameTimer);
        Instance.Register(_navigationService);
        Instance.Register(_gameProgress);
    }

    private void OnDestroy()
    {
        var windowController = Instance.Get<WindowController>();
        windowController.Unsubscribe();

        _services.Clear();
    }

    public T Register<T>(T service) where T : IService
    {
        string serviceName = typeof(T).Name;

        if (_services.ContainsKey(serviceName))
        {
            return (T)_services[serviceName];
        }

        _services.Add(serviceName, service);

        return service;
    }

    public T Get<T>() where T : IService
    {
        string serviceName = typeof(T).Name;

        if(_services.ContainsKey(serviceName))
        {
            return (T)_services[serviceName];
        }

        return default;
    }

    public void UnRegister<T>() where T : IService
    {
        string serviceName = typeof(T).Name;

        if (_services.ContainsKey(serviceName))
        {
            _services.Remove(serviceName);
        }
    }
}
