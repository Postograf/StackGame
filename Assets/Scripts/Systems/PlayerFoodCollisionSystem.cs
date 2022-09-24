using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoodCollisionSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var shared = systems.GetShared<SharedData>();
        var collisions = world.Filter<PlayerFoodCollision>().End();

        var playersPool = world.GetPool<PlayerComponent>();
        var foodPool = world.GetPool<FoodComponent>();
        var collisionsPool = world.GetPool<PlayerFoodCollision>();
        var returnPool = world.GetPool<ReturnToPoolFoodComponent>();

        foreach (var collisionEntity in collisions)
        {
            ref var collision = ref collisionsPool.Get(collisionEntity);
            ref var player =  ref playersPool.Get(collision.Player);
            ref var food = ref foodPool.Get(collision.Food);

            if (player.FoodStack.Count < shared.Config.MaxFoodInStack)
            {
                player.FoodStack.Push(food.RecoverableHealth);
                player.FoodStackUI.Push(food.RecoverableHealth);
                returnPool.Add(collision.Food);
            }

            collisionsPool.Del(collisionEntity);
        }
    }
}
