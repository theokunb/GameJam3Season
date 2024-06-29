public class TerminalViewInteraction : BaseInteraction
{
    public override void Accept(IInteractionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
