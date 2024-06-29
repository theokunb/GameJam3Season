public interface IInteractionVisitor
{
    void Visit(AsteroidsInteraction interaction);
    void Visit(AsteroidsViewInteraction interaction);
    void Visit(DoorInteraction interaction);
    void Visit(DrillInteraction interaction);
    void Visit(TerminalInteraction interaction);
    void Visit(TerminalViewInteraction interaction);
    void Visit(TrashInteraction interaction);
    void Visit(TubeInteraction interaction);
}
