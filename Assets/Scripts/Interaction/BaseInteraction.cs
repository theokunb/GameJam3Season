using System;
using UnityEngine;

public abstract class BaseInteraction : MonoBehaviour
{
    protected Action CompleteAction { get; set; }

    public virtual event Action<BaseInteraction> OnLose;

    public void OnComplete(Action action)
    {
        CompleteAction = action;
    }

    public abstract void Accept(IInteractionVisitor visitor);
}
