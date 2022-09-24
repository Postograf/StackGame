using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class FoodUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public float Value
    {
        set
        {
            var sign = value > 0 ? "+" : "";
            _text.text = $"{sign}{value}";
        }
    }
}
