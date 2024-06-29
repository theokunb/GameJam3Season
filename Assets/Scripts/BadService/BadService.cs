using System.Linq;
using UnityEngine;

public class BadService : MonoBehaviour, IService
{
    [SerializeField] private float _periodInSerconds;
    [SerializeField] private int _maxInteractables;

    private float _elapsedTime;
    private IInteractable[] _notDoors;
    private IInteractable[] _doors;

    private InteractionWeightVisitor _weightVisitor;
    private IServiceVisitor _annoncer;

    private void Start()
    {
        _elapsedTime = 0;
        var allinteractables = FindObjectsOfType<MonoBehaviour>().OfType<IInteractable>().ToArray();

        _notDoors = allinteractables.Where(element => element is not Door).ToArray();
        _doors = allinteractables.Where(element => element is Door).ToArray();

        _weightVisitor = new InteractionWeightVisitor();
        _annoncer = new Annoncer();
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;

        if (_elapsedTime > _periodInSerconds)
        {
            _elapsedTime = 0;
            _weightVisitor.Reset();

            foreach (var element in _notDoors)
            {
                element.Accept(_weightVisitor);
            }

            var random = Random.Range(0, 2);

            if (random == 0)
            {
                random = Random.Range(0,_doors.Length);
                _doors[random].SetStatus(InteractionStatusType.Interactable);
            }
            else if (_weightVisitor.Weight < _maxInteractables)
            {
                random = Random.Range(0, _notDoors.Length);
                _notDoors[random].SetStatus(InteractionStatusType.Interactable);
                _notDoors[random].Accept(_annoncer);
            }
        }
    }
}

public class InteractionWeightVisitor : IServiceVisitor
{
    public float Weight { get; private set; }


    public void Reset()
    {
        Weight = 0;
    }

    public void Visit(Door service)
    {
    }

    public void Visit(Asteroids service)
    {
        if (service.InteractionStatus is Interactable)
        {
            Weight += 1f;
        }
    }

    public void Visit(Drill service)
    {
        if (service.InteractionStatus is Interactable)
        {
            Weight += 1f;
        }
    }

    public void Visit(Terminal service)
    {
        if (service.InteractionStatus is Interactable)
        {
            Weight += 1f;
        }
    }

    public void Visit(Trash service)
    {
        if (service.InteractionStatus is Interactable)
        {
            Weight += 1f;
        }
    }

    public void Visit(Tube service)
    {
        if (service.InteractionStatus is Interactable)
        {
            Weight += 1f;
        }
    }
}

public interface IInteractable
{
    public InteractionStatus InteractionStatus { get; }
    void SetStatus(InteractionStatusType status);
    void Accept(IServiceVisitor visitor);
}

public interface IServiceVisitor
{
    void Visit(Door service);
    void Visit(Asteroids service);
    void Visit(Drill service);
    void Visit(Terminal service);
    void Visit(Trash service);
    void Visit(Tube service);
}

public class Annoncer : IServiceVisitor
{
    private readonly DialogService _dialogService;

    public Annoncer()
    {
        _dialogService = ServiceLoacator.Instance.Get<DialogService>();
    }

    public void Visit(Door service)
    {
        var audiClip = Resources.Load(Constants.Sounds.Door) as AudioClip;
        
        _dialogService.CadetTalk(audiClip);
    }

    public void Visit(Asteroids service)
    {
        var audiClip = Resources.Load(Constants.Sounds.Asteroids) as AudioClip;
        _dialogService.CadetTalk(audiClip);
    }

    public void Visit(Drill service)
    {
        var audiClip = Resources.Load(Constants.Sounds.Drill) as AudioClip;
        _dialogService.CadetTalk(audiClip);
    }

    public void Visit(Terminal service)
    {
        var audiClip = Resources.Load(Constants.Sounds.Terminal) as AudioClip;
        _dialogService.CadetTalk(audiClip);
    }

    public void Visit(Trash service)
    {
        var audiClip = Resources.Load(Constants.Sounds.Trash) as AudioClip;
        _dialogService.CadetTalk(audiClip);
    }

    public void Visit(Tube service)
    {
        var audiClip = Resources.Load(Constants.Sounds.Tube) as AudioClip;
        _dialogService.CadetTalk(audiClip);
    }
}