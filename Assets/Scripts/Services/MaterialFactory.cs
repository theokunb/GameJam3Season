using UnityEngine;

public class MaterialFactory : IService
{
    private DoorMaterialVistor _visitor;

    public MaterialFactory()
    {
        _visitor = new DoorMaterialVistor();
    }

    public Material GetMaterial(InteractionStatus status, UnlockDifficultType unlockDifficult)
    {
        _visitor.UnlockDifficultType = unlockDifficult;
        status.Accept(_visitor);

        return _visitor.CurrentMaterial;
    }
}

public class DoorMaterialVistor : IInteractionStatusVisitor
{
    public UnlockDifficultType UnlockDifficultType { get; set; }

    public Material CurrentMaterial {  get; private set; }

    public void Visit(NotInteractable door)
    {
        CurrentMaterial = Resources.Load<Material>(Constants.Materials.DoorStatusGreen);
    }

    public void Visit(Interactable door)
    {
        switch (UnlockDifficultType)
        {
            case UnlockDifficultType.Level1:
                CurrentMaterial = Resources.Load<Material>(Constants.Materials.DoorStatusYellow);
                break;
            case UnlockDifficultType.Level2:
                CurrentMaterial = Resources.Load<Material>(Constants.Materials.DoorStatusYellow);
                break;
            case UnlockDifficultType.Level3:
                CurrentMaterial = Resources.Load<Material>(Constants.Materials.DoorStatusRed);
                break;
            case UnlockDifficultType.Level4:
                CurrentMaterial = Resources.Load<Material>(Constants.Materials.DoorStatusRed);
                break;
        }
    }
}
