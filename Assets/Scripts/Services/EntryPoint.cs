using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private List<OrderedMonobeh> _elements;

    private List<OrderedMonobeh> _sorted;

    private void Awake()
    {
        _sorted = _elements.OrderBy(element => element.Order).ToList();

        foreach (var element in _sorted)
        {
            element.OrderedAwake();
        }
    }
}
