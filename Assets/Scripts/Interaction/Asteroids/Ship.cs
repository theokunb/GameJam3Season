using System;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float _speed;

    private NewInput _input;
    private Rigidbody2D _rigidbody;

    public event Action Died;

    private void Awake()
    {
        _input = new NewInput();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void FixedUpdate()
    {
        var move = _input.Player.Movement.ReadValue<Vector2>();

        if (move.sqrMagnitude < 0.1f)
        {
            _rigidbody.velocity = Vector2.zero;
        }

        _rigidbody.velocity = _speed * move * Time.fixedDeltaTime;
    }

    public void Die()
    {
        Died?.Invoke();
    }
}
