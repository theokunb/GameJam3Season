using UnityEngine;

public class Player : MonoBehaviour, IService
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private NewInput _input;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _input = new NewInput();
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        _animator.SetFloat(Constants.AnimationParams.Speed, move.sqrMagnitude);

        if (move.magnitude > 0)
        {
            var movement = new Vector3(move.x, 0, move.y) * _speed * Time.fixedDeltaTime;

            _rigidBody.velocity = movement;
        }
        else
        {
            _rigidBody.velocity = Vector3.zero;
        }

        if(move.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if(move.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
}
