using UnityEngine;

public enum UnlockDifficultType
{
    Level1,
    Level2,
    Level3,
    Level4
}

public static class UnlockDifficultExtension
{
    public static UnlockDifficult ToUnlockDifficult(this UnlockDifficultType difficult)
    {
        switch (difficult)
        {
            case UnlockDifficultType.Level1:
                return new UnlockDifficultLevel1();
            case UnlockDifficultType.Level2:
                return new UnlockDifficultLevel2();
            case UnlockDifficultType.Level3:
                return new UnlockDifficultLevel3();
            case UnlockDifficultType.Level4:
                return new UnlockDifficultLevel4();
            default:
                return null;
        }
    }
}

public abstract class UnlockDifficult
{
    public abstract void Accept(IUnlockDifficultVisitor visitor);
}

public class UnlockDifficultLevel1 : UnlockDifficult
{
    public override void Accept(IUnlockDifficultVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class UnlockDifficultLevel2 : UnlockDifficult
{
    public override void Accept(IUnlockDifficultVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class UnlockDifficultLevel3 : UnlockDifficult
{
    public override void Accept(IUnlockDifficultVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class UnlockDifficultLevel4 : UnlockDifficult
{
    public override void Accept(IUnlockDifficultVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public interface IUnlockDifficultVisitor
{
    void Visit(UnlockDifficultLevel1 unlockDifficult);
    void Visit(UnlockDifficultLevel2 unlockDifficult);
    void Visit(UnlockDifficultLevel3 unlockDifficult);
    void Visit(UnlockDifficultLevel4 unlockDifficult);
}

public class UnlockDifficultVisitor<T> : Creator<T>, IUnlockDifficultVisitor where T : BaseInteraction
{
    public UnlockDifficultVisitor(Canvas canvas) : base(canvas)
    {
    }

    protected override void OnCreating()
    {
        CurrentInteraction.OnComplete(() =>
        {
            var currentDoor = ServiceLoacator.Instance.Get<Door>();
            var score = ServiceLoacator.Instance.Get<Score>();

            currentDoor.SetStatus(InteractionStatusType.NotInteractable);
            CurrentInteraction.Accept(score);

            HideInteraction();
        });
    }

    protected override void OnHiding()
    {
    }

    public void Visit(UnlockDifficultLevel1 unlockDifficult)
    {
        var prefab = Resources.Load<GameObject>(Constants.Interactions.DoorLevel1);

        CreateInteraction(prefab);
    }

    public void Visit(UnlockDifficultLevel2 unlockDifficult)
    {
        var prefab = Resources.Load<GameObject>(Constants.Interactions.DoorLevel2);

        CreateInteraction(prefab);
    }

    public void Visit(UnlockDifficultLevel3 unlockDifficult)
    {
        var prefab = Resources.Load<GameObject>(Constants.Interactions.DoorLevel3);

        CreateInteraction(prefab);
    }

    public void Visit(UnlockDifficultLevel4 unlockDifficult)
    {
        var prefab = Resources.Load<GameObject>(Constants.Interactions.DoorLevel4);

        CreateInteraction(prefab);
    }
}
