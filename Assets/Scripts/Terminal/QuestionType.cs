public enum QuestionType
{
    Arithmetic,
    Logic
}

public static class QuestionExtension
{
    public static Question ToQuestion(this QuestionType question)
    {
        switch (question)
        {
            case QuestionType.Arithmetic:
                return new ArithmeticQuestion();
            case QuestionType.Logic:
                return new LogicQuestion();
            default:
                return null;

        }
    }
}