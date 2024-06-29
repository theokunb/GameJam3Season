public abstract class Question
{
    public abstract void Accept(IQuestionVisitor visitor);
}

public class ArithmeticQuestion : Question
{
    public override void Accept(IQuestionVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class LogicQuestion : Question
{
    public override void Accept(IQuestionVisitor visitor)
    {
        visitor.Visit(this);
    }
}