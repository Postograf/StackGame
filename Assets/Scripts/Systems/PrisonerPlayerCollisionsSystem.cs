using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerPlayerCollisionsSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var shared = systems.GetShared<SharedData>();
        var collisions = world.Filter<PrisonerPlayerCollision>().End();

        var playersPool = world.GetPool<PlayerComponent>();
        var prisonersPool = world.GetPool<PrisonerComponent>();
        var collisionsPool = world.GetPool<PrisonerPlayerCollision>();

        foreach (var collisionEntity in collisions)
        {
            ref var collision = ref collisionsPool.Get(collisionEntity);
            ref var player = ref playersPool.Get(collision.Player);
            ref var prisoner = ref prisonersPool.Get(collision.Prisoner);

            if (
                prisoner.DelayForFood <= 0 
                && player.FoodStack.Count > 0 
                && prisoner.Health < prisoner.MaxHealth
                && prisoner.Health > 0
            )
            {
                prisoner.Health += player.FoodStack.Pop();
                player.FoodStackUI.Pop();
                prisoner.HealthBar.Filling = prisoner.Health / prisoner.MaxHealth;
                prisoner.DelayForFood = shared.Config.DelayForFood;
            }
        }
    }
}
