using System;

public class Score : IInteractionVisitor, IService
{
    private const float Point = 0.01f;

    private float _currentScore;

    public float CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            ScoreChanged?.Invoke(CurrentScore);

            if(CurrentScore >= 1)
            {
                var finishService = ServiceLoacator.Instance.Get<FinishService>();

                finishService.RaiseEvent();
            }
        }
    }

    public event Action<float> ScoreChanged;

    public Score()
    {
        _currentScore = 0f;
    }

    public void Visit(AsteroidsInteraction interaction)
    {
        CurrentScore += 11 * Point;
    }

    public void Visit(AsteroidsViewInteraction interaction)
    {
    }

    public void Visit(DoorInteraction interaction)
    {
        CurrentScore += 3 * Point;
    }

    public void Visit(DrillInteraction interaction)
    {
        CurrentScore += 8 * Point;
    }

    public void Visit(TerminalInteraction interaction)
    {
        CurrentScore += 10 * Point;
    }

    public void Visit(TerminalViewInteraction interaction)
    {
    }

    public void Visit(TrashInteraction interaction)
    {
        CurrentScore += 8 * Point;
    }

    public void Visit(TubeInteraction interaction)
    {
        CurrentScore += 12 * Point;
    }
}