using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float _timeInSeconds;

    private float _elapsedTime;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _elapsedTime = 0;
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;

        var currentValue = (_timeInSeconds - _elapsedTime) / _timeInSeconds;
        _slider.value = currentValue;
    }
}
