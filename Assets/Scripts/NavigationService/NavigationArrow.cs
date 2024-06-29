using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class NavigationArrow : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _scaleDistance;

    private Transform _target;
    private SpriteRenderer _image;
    private float _minScale = 0.7f;
    private float _maxScale = 1.5f;

    private void Awake()
    {
        _image = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        if(_target == null)
        {
            return;
        }

        var distance = Vector3.Distance(transform.position, _target.position);
        var attitudeScale = Mathf.Abs(_maxScale - (distance / _scaleDistance));
        
        if(attitudeScale < _minScale)
        {
            attitudeScale = _minScale;
        }
        else if(attitudeScale > _maxScale)
        {
            attitudeScale = _maxScale;
        }
        transform.localScale = Vector3.one * attitudeScale;

        TweenerCore<Color, Color, ColorOptions> task = null;

        if (distance < _minDistance)
        {
            if(_image.color.a != 0 && task == null)
            {
                task = _image.DOFade(0, 0.2f).OnComplete(() =>
                {
                    task = null;
                });
            }

            return;
        }
        else
        {
            if (_image.color.a == 0 && task == null)
            {
                task = _image.DOFade(1, 0.2f).OnComplete(() =>
                {
                    task = null;
                });
            }
        }

        var direction = _target.position - transform.position;
        var angle = Quaternion.LookRotation(direction);

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.forward * _radius + transform.up / 10, Time.fixedDeltaTime * _moveSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.fixedDeltaTime * _rotationSpeed);
    }
}
