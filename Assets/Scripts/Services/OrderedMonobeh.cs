using UnityEngine;

public class OrderedMonobeh : MonoBehaviour
{
    [SerializeField] private int _order;

    public int Order => _order;

    public virtual void OrderedAwake()
    {
    }
}
