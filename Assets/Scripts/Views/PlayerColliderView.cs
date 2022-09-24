using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerColliderView : ColliderView
{
    private void OnTriggerEnter(Collider other)
    {
        var food = SharedData.GetEntity(other.gameObject);
        if (World.GetPool<FoodComponent>().Has(food))
        {
            var collision = World.NewEntity();
            ref var collisionComponent = ref World.GetPool<PlayerFoodCollision>().Add(collision);
            collisionComponent.Player = SharedData.GetEntity(gameObject);
            collisionComponent.Food = food;
        }
    }
}
