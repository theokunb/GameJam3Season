using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCompleteVisitor : IInteractionVisitor, IService
{
    private DialogService _dialogService;
    private DialogService DialogService
    {
        get
        {
            if(_dialogService == null)
            {
                _dialogService = ServiceLoacator.Instance.Get<DialogService>();
            }

            return _dialogService;
        }
    }

    public void Visit(AsteroidsInteraction interaction)
    {
        var audioClip = Resources.Load(Constants.Sounds.Complete) as AudioClip;

        DialogService.CadetTalk(audioClip, true);
    }

    public void Visit(AsteroidsViewInteraction interaction)
    {
        var audioClip = Resources.Load(Constants.Sounds.Complete) as AudioClip;

        DialogService.CadetTalk(audioClip, true);
    }

    public void Visit(DoorInteraction interaction)
    {
    }

    public void Visit(DrillInteraction interaction)
    {
        var audioClip = Resources.Load(Constants.Sounds.Complete) as AudioClip;

        DialogService.CadetTalk(audioClip, true);
    }

    public void Visit(TerminalInteraction interaction)
    {
        var audioClip = Resources.Load(Constants.Sounds.Complete) as AudioClip;

        DialogService.CadetTalk(audioClip, true);
    }

    public void Visit(TerminalViewInteraction interaction)
    {
        var audioClip = Resources.Load(Constants.Sounds.Complete) as AudioClip;

        DialogService.CadetTalk(audioClip, true);
    }

    public void Visit(TrashInteraction interaction)
    {
        var audioClip = Resources.Load(Constants.Sounds.Complete) as AudioClip;

        DialogService.CadetTalk(audioClip, true);
    }

    public void Visit(TubeInteraction interaction)
    {
        var audioClip = Resources.Load(Constants.Sounds.Complete) as AudioClip;

        DialogService.CadetTalk(audioClip, true);
    }
}
