using UnityEngine;

public class TrashInteraction : BaseInteraction
{
    [SerializeField] private Transform _point;
    [SerializeField] private float _yRandom;
    [SerializeField] private float _xRandom;
    
    private TrashElement[] _trashes;
    private int _cleaned;
    private SoundContainer _soundContainer;
    private AudioClip _audioClip;

    private void Awake()
    {
        _trashes = GetComponentsInChildren<TrashElement>();
        _cleaned = 0;
    }

    private void OnEnable()
    {
        foreach(var trash in _trashes)
        {
            trash.Click += OnTrashClick;
            trash.Cleaned += OnTrashCleaned;
        }
    }

    private void OnDisable()
    {
        foreach(var trash in _trashes)
        {
            trash.Click -= OnTrashClick;
            trash.Cleaned -= OnTrashCleaned;
        }
    }

    private void Start()
    {
        _soundContainer = ServiceLoacator.Instance.Get<SoundContainer>();
        _audioClip = Resources.Load(Constants.Sounds.Sucked) as AudioClip;
    }

    private void OnTrashClick(TrashElement trash)
    {
        var x = Random.Range(-_xRandom, _xRandom);
        var y = Random.Range(-_yRandom, _yRandom);

        trash.SetTarget(_point.position + new Vector3(x, y, 0));
    }

    private void OnTrashCleaned(TrashElement trash)
    {
        trash.Cleaned -= OnTrashCleaned;
        _cleaned++;

        _soundContainer.Play(_audioClip, conf =>
        {
            conf.loop = false;
            conf.volume = 0.1f;
        });

        if(_cleaned == _trashes.Length)
        {
            var success = Resources.Load(Constants.Sounds.Success) as AudioClip;

            _soundContainer.Play(success, conf =>
            {
                conf.loop = false;
                conf.volume = 0.1f;
            });

            CompleteAction?.Invoke();
        }
    }

    public override void Accept(IInteractionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
