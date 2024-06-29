public class AsteroidsViewInteraction : BaseInteraction
{
    public override void Accept(IInteractionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
