using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PrisonerComponent
{
    private float _delayForFood;
    public float DelayForFood
    {
        get => _delayForFood;
        set => _delayForFood = Mathf.Max(value, 0);
    }

    private float _health;
    public float Health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, MaxHealth);
    }

    public float MaxHealth;
    public HealthBar HealthBar;
    public Animator Animator;
}
