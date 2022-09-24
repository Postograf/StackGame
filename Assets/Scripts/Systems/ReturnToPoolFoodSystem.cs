using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPoolFoodSystem : IEcsRunSystem
{
    private FoodSpawner _spawner;

    public ReturnToPoolFoodSystem(FoodSpawner spawner)
    {
        _spawner = spawner;
    }

    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var gameObjectsPool = world.GetPool<GameObjectComponent>();
        var poolingFood = world
            .Filter<ReturnToPoolFoodComponent>()
            .Inc<GameObjectComponent>()
            .Inc<FoodComponent>()
            .End();

        foreach (var foodEntity in poolingFood)
        {
            ref var gameObject = ref gameObjectsPool.Get(foodEntity);

            _spawner.ReturnFood(gameObject.GameObject);
            systems.GetShared<SharedData>().RemoveGameObject(gameObject.GameObject);
            world.DelEntity(foodEntity);
        }
    }
}
