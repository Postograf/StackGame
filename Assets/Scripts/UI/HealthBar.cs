using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _filling;

    public float Filling
    {
        get => _filling.fillAmount;
        set
        {
            _filling.fillAmount = Mathf.Clamp01(value);
        }
    }
}
