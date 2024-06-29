using UnityEngine;

public class DoorIndicator : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private MaterialFactory _materialFactory;

    private MaterialFactory MaterialFactory
    {
        get
        {
            if(_materialFactory == null)
            {
                _materialFactory = ServiceLoacator.Instance.Get<MaterialFactory>();
            }

            return _materialFactory;
        }
    }

    public InteractionStatus CurrentDoorStatus { get; private set; }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetStatus(InteractionStatus doorStatus, UnlockDifficultType unlockDifficult)
    {
        var material = MaterialFactory.GetMaterial(doorStatus, unlockDifficult);
        CurrentDoorStatus = doorStatus;
        
        _meshRenderer.material = material;
    }
}
