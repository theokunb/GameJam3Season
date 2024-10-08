using UnityEngine;
using UnityEngine.UI;

public class GameTimer : OrderedMonobeh, IService
{
    [SerializeField] private float _timeInSeconds;

    private float _elapsedTime;
    private Slider _slider;

    public override void OrderedAwake()
    {
        _slider = GetComponent<Slider>();
        _elapsedTime = 0;
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;

        var currentValue = (_timeInSeconds - _elapsedTime) / _timeInSeconds;
        _slider.value = currentValue;

        if(_timeInSeconds - _elapsedTime <= 0)
        {
            var gameMenu = ServiceLoacator.Instance.Get<GameMenu>();

            gameMenu.SetDescription(Lean.Localization.LeanLocalization.GetTranslationText(Constants.Translations.TimeUp));
            gameMenu.gameObject.SetActive(true);
        }
    }
}
