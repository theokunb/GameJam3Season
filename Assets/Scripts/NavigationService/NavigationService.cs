using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavigationService : MonoBehaviour, IService
{
    [SerializeField] private float _updatePeriod;

    private IInteractable[] _interactables;
    private IServiceVisitor _visitor;
    private float _elapsedTIme;

    private void Start()
    {
        _visitor = new NavigationVisitor();
    }

    private void FixedUpdate()
    {
        _elapsedTIme += Time.fixedDeltaTime;

        if(_elapsedTIme < _updatePeriod)
        {
            return;
        }

        _elapsedTIme = 0;
        _interactables = FindObjectsOfType<MonoBehaviour>().OfType<IInteractable>().ToArray();

        foreach (var interactable in _interactables)
        {
            interactable.Accept(_visitor);
        }
    }
}

public class ArrowStatusVisitor : IInteractionStatusVisitor
{
    public NavigationArrow CurrentArrow { get; set; }

    public void Visit(NotInteractable obj)
    {
        CurrentArrow.gameObject.SetActive(false);
    }

    public void Visit(Interactable obj)
    {
        CurrentArrow.gameObject.SetActive(true);
    }
}

public class NavigationVisitor : IServiceVisitor
{
    private ArrowStatusVisitor _visitor;
    private GameObject _prefab;
    private Transform _parent;
    private Dictionary<string, NavigationArrow> _dic;

    public NavigationVisitor()
    {
        _visitor = new ArrowStatusVisitor();
        _dic = new Dictionary<string, NavigationArrow>();
        _prefab = Resources.Load(Constants.Prefabs.Arrow) as GameObject;

        var player = ServiceLoacator.Instance.Get<Player>();
        _parent = player.transform;
    }

    public void Visit(Door service)
    {
    }

    public void Visit(Asteroids service)
    {
        if (!_dic.ContainsKey(service.gameObject.name))
        {
            var navArrow = CreateNavigationArrow();
            _dic.Add(service.gameObject.name, navArrow);
            navArrow.SetTarget(service.transform);
        }

        _visitor.CurrentArrow = _dic[service.gameObject.name];
        service.InteractionStatus.Accept(_visitor);
    }

    public void Visit(Drill service)
    {
        if (!_dic.ContainsKey(service.gameObject.name))
        {
            var navArrow = CreateNavigationArrow();
            _dic.Add(service.gameObject.name, navArrow);
            navArrow.SetTarget(service.transform);
        }

        _visitor.CurrentArrow = _dic[service.gameObject.name];
        service.InteractionStatus.Accept(_visitor);
    }

    public void Visit(Terminal service)
    {
        if (!_dic.ContainsKey(service.gameObject.name))
        {
            var navArrow = CreateNavigationArrow();
            _dic.Add(service.gameObject.name, navArrow);
            navArrow.SetTarget(service.transform);
        }

        _visitor.CurrentArrow = _dic[service.gameObject.name];
        service.InteractionStatus.Accept(_visitor);
    }

    public void Visit(Trash service)
    {
        if (!_dic.ContainsKey(service.gameObject.name))
        {
            var navArrow = CreateNavigationArrow();
            _dic.Add(service.gameObject.name, navArrow);
            navArrow.SetTarget(service.transform);
        }

        _visitor.CurrentArrow = _dic[service.gameObject.name];
        service.InteractionStatus.Accept(_visitor);
    }

    public void Visit(Tube service)
    {
        if (!_dic.ContainsKey(service.gameObject.name))
        {
            var navArrow = CreateNavigationArrow();
            _dic.Add(service.gameObject.name, navArrow);
            navArrow.SetTarget(service.transform);
        }

        _visitor.CurrentArrow = _dic[service.gameObject.name];
        service.InteractionStatus.Accept(_visitor);
    }

    private NavigationArrow CreateNavigationArrow()
    {
        var instance = Object.Instantiate(_prefab, _parent);

        return instance.GetComponent<NavigationArrow>();
    }
}