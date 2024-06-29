using UnityEngine;
using UnityEngine.UI;

public class GameProgress : MonoBehaviour, IService
{
    private Slider _slider;
    private Score _score;


    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _score = ServiceLoacator.Instance.Get<Score>();

        _score.ScoreChanged += OnScoreChanged;
    }

    private void OnDestroy()
    {
        _score.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(float value)
    {
        _slider.value = value;
    }
}
