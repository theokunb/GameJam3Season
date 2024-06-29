public interface IInteractionStatusVisitor
{
    void Visit(NotInteractable obj);
    void Visit(Interactable obj);
}
