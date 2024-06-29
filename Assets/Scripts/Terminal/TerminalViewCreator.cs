using UnityEngine;

public class TerminalViewCreator<T> : Creator<T> where T : BaseInteraction
{
    public TerminalViewCreator(Canvas canvas) : base(canvas)
    {
    }

    protected override void OnCreating()
    {
    }

    protected override void OnHiding()
    {
    }
}
