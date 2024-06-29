using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _speed;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ship ship))
        {
            ship.Die();
        }

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.right * _speed * Time.fixedDeltaTime;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
