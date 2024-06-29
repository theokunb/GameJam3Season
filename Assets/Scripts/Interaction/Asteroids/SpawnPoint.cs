using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Ship _ship;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _spawnPeriod;


    private GameObject _prefab;
    private Transform[] _childs;
    private float _elapsedTime;

    private void Start()
    {
        _childs = GetComponentsInChildren<Transform>();
        _prefab = Resources.Load(Constants.Interactions.Asteroid) as GameObject;

        _elapsedTime = 0;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _spawnPeriod)
        {
            _elapsedTime = 0;

            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        int randPoint = Random.Range(1, _childs.Length);

        var instance = Instantiate(_prefab, _parent);
        instance.transform.position = _childs[randPoint].position;
        instance.transform.rotation = _childs[randPoint].rotation;

        if(instance.TryGetComponent(out Asteroid asteroid))
        {
            float speed = Random.Range(_minSpeed, _maxSpeed);

            asteroid.SetSpeed(speed);
        }
    }
}
