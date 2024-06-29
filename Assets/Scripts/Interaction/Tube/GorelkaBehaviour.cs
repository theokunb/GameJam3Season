using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorelkaBehaviour : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _moveTime;

    private int _side;
    private float _currentSpeed;
    private float _elapsedTime;
    private NewInput _newInput;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _newInput = new NewInput();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InitParams();

        _elapsedTime = 0;
    }

    private void OnEnable()
    {
        _newInput.Enable();
    }

    private void OnDisable()
    {
        _newInput.Disable();
    }

    private void FixedUpdate()
    {
        float currentResolution = Screen.width * Screen.height;
        float targetResolution = 1920 * 1080;
        float factor = currentResolution / targetResolution > 1 ? (currentResolution / targetResolution) / 1.6f : 1;

        var move = _newInput.Player.Movement.ReadValue<Vector2>();
        _elapsedTime += Time.fixedDeltaTime;
        
        if(_elapsedTime > _moveTime)
        {
            InitParams();
            _elapsedTime = 0;
        }

        _rb.velocity = _currentSpeed * new Vector2(_side, 0) * Time.fixedDeltaTime * factor + 
            move * _moveSpeed * Time.fixedDeltaTime * factor;
    }


    private void InitParams()
    {
        int rand = Random.Range(0, 2);

        if(rand == 0)
        {
            _side = -1;
        }
        else
        {
            _side = 1;
        }

        _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
    }
}
