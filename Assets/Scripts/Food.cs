using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Food
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _recoverableHealth;

    public float RecoverableHealth => _recoverableHealth;
    public GameObject GameObject => _gameObject;

    public Food(GameObject gameObject, float recoverableHealth)
    {
        _gameObject = gameObject;
        _recoverableHealth = recoverableHealth;
    }
}
