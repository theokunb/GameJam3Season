using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private int _direction;

    private void Start()
    {
        var rand = Random.Range(0, 2);

        if(rand == 0)
        {
            _direction = 1;
        }
        else
        {
            _direction = -1;
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, _rotationSpeed) * Time.fixedDeltaTime * _direction);
    }
}
