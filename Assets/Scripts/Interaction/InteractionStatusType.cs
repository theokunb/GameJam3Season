public enum InteractionStatusType
{
    NotInteractable,
    Interactable
}

public abstract class InteractionStatus
{
    public abstract void Accept(IInteractionStatusVisitor visitor);
}

/// <summary>
/// not interactable status
/// </summary>
public class NotInteractable : InteractionStatus
{
    public override void Accept(IInteractionStatusVisitor visitor)
    {
        visitor.Visit(this);
    }
}

/// <summary>
/// interactable status
/// </summary>
public class Interactable : InteractionStatus
{
    public override void Accept(IInteractionStatusVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public static class InteractionStatusExtension
{
    public static InteractionStatus ToInteractableStatus(this InteractionStatusType status)
    {
        switch(status)
        {
            case InteractionStatusType.NotInteractable:
                return new NotInteractable();
            case InteractionStatusType.Interactable:
                return new Interactable();
            default:
                return null;
        }
    }
}