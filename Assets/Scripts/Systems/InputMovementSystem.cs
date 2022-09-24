using Leopotam.EcsLite;

using UnityEngine;

class InputMovementSystem : IEcsRunSystem
{
    private string _speedAnimation = "Speed_f";

    public void Run (IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var config = systems.GetShared<SharedData>().Config;

        var movementPool = world.GetPool<InputMovementComponent>();
        var movablesPool = world.GetPool<MovableComponent>();
        var playersPool = world.GetPool<PlayerComponent>();
        var foodPool = world.GetPool<FoodComponent>();

        var movables = world.Filter<MovableComponent>().End();
        var inputMoves = world.Filter<InputMovementComponent>().End();

        foreach (var move in inputMoves)
        {
            var direction = movementPool.Get(move).Direction.normalized;

            foreach (var entity in movables)
            {
                ref var movable = ref movablesPool.Get(entity);

                if (playersPool.Has(entity))
                {
                    ref var player = ref playersPool.Get(entity);
                    player.Animator?.SetFloat(_speedAnimation, direction.magnitude * config.PlayerSpeed);
                    movable.Rigidbody.velocity = direction * config.PlayerSpeed;

                    if (direction.sqrMagnitude > 0)
                        movable.Transform.forward = direction;
                }
                else if (foodPool.Has(entity))
                {
                    ref var food = ref foodPool.Get(entity);
                    var foodDirection = food.InputDirectionOffset * direction;
                    movable.Rigidbody.velocity = foodDirection * food.Speed;

                    if (foodDirection.sqrMagnitude > 0)
                        movable.Transform.forward = foodDirection;
                }
            }
        }
    }
}