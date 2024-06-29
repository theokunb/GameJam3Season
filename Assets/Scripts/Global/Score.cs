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
        }
    }

    public event Action<float> ScoreChanged;

    public Score()
    {
        _currentScore = 0;
    }

    public void Visit(AsteroidsInteraction interaction)
    {
        CurrentScore += 2 * Point;
    }

    public void Visit(AsteroidsViewInteraction interaction)
    {
    }

    public void Visit(DoorInteraction interaction)
    {
        CurrentScore += Point;
    }

    public void Visit(DrillInteraction interaction)
    {
        CurrentScore += 2 * Point;
    }

    public void Visit(TerminalInteraction interaction)
    {
        CurrentScore += 3 * Point;
    }

    public void Visit(TerminalViewInteraction interaction)
    {
    }

    public void Visit(TrashInteraction interaction)
    {
        CurrentScore += 2 * Point;
    }

    public void Visit(TubeInteraction interaction)
    {
        CurrentScore += 2 * Point;
    }
}