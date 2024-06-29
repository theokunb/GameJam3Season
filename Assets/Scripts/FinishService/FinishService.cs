using UnityEngine;

public class FinishService : MonoBehaviour, IService
{
    public void RaiseEvent()
    {
        var badService = ServiceLoacator.Instance.Get<BadService>();
        badService.enabled = false;


    }
}
