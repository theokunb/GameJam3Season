using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextSizeNormaliztor : MonoBehaviour
{
    [SerializeField] private TMP_Text[] objects;

    private void Start()
    {
        if (objects.Length == 0)
            return;

        var min = objects.First().fontSize;

        foreach(var obj in objects)
        {
            if(obj.fontSize < min)
            {
                min = obj.fontSize;
            }
        }

        foreach(var obj in objects)
        {
            obj.enableAutoSizing = false;
            obj.fontSize = min;
        }
    }
}
