using UnityEngine;
using UnityEngine.UI;

public class TubeInteraction : BaseInteraction
{
    [SerializeField] private Crack _crack;
    [SerializeField] private Image _progress;

    private bool _colliding;
    private bool _completed = false;
    private AudioClip _success;
    private SoundContainer _soundContainer;

    private void OnEnable()
    {
        _crack.Collided += OnCrackCollided;
        _crack.ColliderExited += OnColliderExited;
    }

    private void OnDisable()
    {
        _crack.Collided -= OnCrackCollided;
        _crack.ColliderExited -= OnColliderExited;
    }

    private void Start()
    {
        _soundContainer = ServiceLoacator.Instance.Get<SoundContainer>();
        _success = Resources.Load(Constants.Sounds.Success) as AudioClip;
    }

    private void Update()
    {
        if (!_colliding)
        {
            _progress.fillAmount -= 0.05f * Time.deltaTime;
        }
    }

    private void OnColliderExited()
    {
        _colliding = false;
    }


    private void OnCrackCollided()
    {
        _colliding = true;
        _progress.fillAmount += 0.1f * Time.deltaTime;

        if (_progress.fillAmount == 1 && _completed == false)
        {
            _soundContainer.Play(_success, conf =>
            {
                conf.loop = false;
                conf.volume = 0.1f;
            });
            _completed = true;
            CompleteAction?.Invoke();
        }
    }

    public override void Accept(IInteractionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
