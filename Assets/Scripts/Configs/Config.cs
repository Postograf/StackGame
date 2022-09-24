using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Configuration")]
public class Config : ScriptableObject
{
    [Header("Spawner")]
    public List<Food> FoodPrefabs;
    public float SpawnDelay;
    public Vector3 SpawnAreaTopRightCorner;
    public Vector3 SpawnAreaBottomLeftCorner;

    [Header("Food")]
    public float MinSpeed;
    public float MaxSpeed;

    [Header("Player")]
    public float PlayerSpeed;
    public int MaxFoodInStack;

    [Header("Prisoner")]
    public float DelayForFood;
    public float MaxHealth;
    public float LossHealthPerSecond;
}