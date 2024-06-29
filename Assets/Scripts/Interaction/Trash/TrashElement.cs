using System;
using UnityEngine;

public class TrashElement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Vector3? _targetPosition;

    public event Action<TrashElement> Click;
    public event Action<TrashElement> Cleaned;

    private void FixedUpdate()
    {
        if (_targetPosition == null)
            return;

        if(Vector3.Distance(transform.position, _targetPosition.Value) < 1)
        {
            Cleaned?.Invoke(this);

            if(TryGetComponent(out AsteroidRotation rotation))
            {
                rotation.enabled = false;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition.Value, _moveSpeed * Time.fixedDeltaTime);
    }

    public void OnClick()
    {
        Click.Invoke(this);
    }

    public void SetTarget(Vector3 target)
    {
        _targetPosition = target;
    }
}
