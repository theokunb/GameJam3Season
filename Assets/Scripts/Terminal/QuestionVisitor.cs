using Unity.VisualScripting;
using UnityEngine;

public class QuestionVisitor : IQuestionVisitor
{
    private readonly TerminalCreator<TerminalInteraction> _terminalCreator;
    private readonly TerminalViewCreator<TerminalViewInteraction> _terminalViewCreator;
    private readonly TerminalMediator _mediator;

    public QuestionVisitor()
    {
        var textureCanvas = ServiceLoacator.Instance.Get<TextureRendererCanvas>();
        var mainCanvas = ServiceLoacator.Instance.Get<GlobalCanvas>();
        
        _terminalCreator = new TerminalCreator<TerminalInteraction>(textureCanvas.Canvas);
        _terminalViewCreator = new TerminalViewCreator<TerminalViewInteraction>(mainCanvas.Canvas);

        _mediator = new TerminalMediator(_terminalViewCreator, _terminalCreator);
        _mediator.Act();
    }

    private void ConfigureQuestion(QuestionDto question)
    {
        var prefab = Resources.Load(Constants.Interactions.Terminal) as GameObject;
        var viewPrefab = Resources.Load(Constants.Interactions.TerminalView) as GameObject;
        var windows = ServiceLoacator.Instance.Get<WindowController>();

        _terminalCreator.CreateInteraction(prefab);
        _terminalViewCreator.CreateInteraction(viewPrefab);

        _terminalCreator.SetQuestion(interaction =>
        {
            windows.Push(_terminalCreator);
            interaction.SetQuestion(question);
        });
    }

    public void Visit(ArithmeticQuestion question)
    {
        int randomNum1 = Random.Range(0, 256);
        int randomNum2 = Random.Range(0, 256);
        int res;

        int operationCode = Random.Range(0, 3);
        char operationCh;

        switch (operationCode)
        {
            case 0:
                {
                    operationCh = '+';
                    res = randomNum1 + randomNum2;
                    break;
                }
            case 1:
                {
                    operationCh = '-';
                    res = randomNum1 - randomNum2;
                    break;
                }
            default:
                {
                    operationCh = '*';
                    res = randomNum1 * randomNum2;
                    break;
                }
        }

        ConfigureQuestion(new QuestionDto($"| user log:\n| > theokunb\n| > Welcome to Accessmaster 9000\n| > Please write {randomNum1} {operationCh} {randomNum2}", $"{res}"));
    }

    public void Visit(LogicQuestion question)
    {
        string[] words = { "user", "log", "theokunb", "Welcome" , "to" , "Accessmaster", "9000", "which",  "of", "these" , "words", "is", "the", "password" };
        int rand = Random.Range(0,words.Length);

        ConfigureQuestion(new QuestionDto($"| {words[0]} {words[1]}:\n| > {words[2]}\n| > {words[3]} {words[4]} {words[5]} {words[6]}\n| > {words[7]} {words[8]} {words[9]} {words[10]} {words[11]} {words[12]} {words[13]}", words[rand]));
    }
}

public class QuestionDto
{
    public string Question { get; private set; }
    public string Answer { get; private set; }

    public QuestionDto(string question, string answer)
    {
        Question = question;
        Answer = answer;
    }

}
public interface IQuestionVisitor
{
    void Visit(ArithmeticQuestion question);
    void Visit(LogicQuestion question);
}