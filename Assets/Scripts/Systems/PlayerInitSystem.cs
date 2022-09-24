using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
    private GameObject _player;
    private FoodStackUI _foodStackUI;

    public PlayerInitSystem(GameObject player, FoodStackUI foodStack)
    {
        _player = player;
        _foodStackUI = foodStack;
    }

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var playerEntity = world.NewEntity();

        ref var playerComponent = ref world.GetPool<PlayerComponent>().Add(playerEntity);
        playerComponent.Animator = _player.GetComponent<Animator>();
        playerComponent.FoodStack = new Stack<float>();
        playerComponent.FoodStackUI = _foodStackUI;

        systems.GetShared<SharedData>().AddEntityGameObject(playerEntity, _player);

        if (_player.TryGetComponent(out PlayerColliderView playerCollider))
        {
            playerCollider.World = world;
            playerCollider.SharedData = systems.GetShared<SharedData>();
        }

        ref var movableComponent = ref world.GetPool<MovableComponent>().Add(playerEntity);
        movableComponent.Transform = _player.transform;
        movableComponent.Rigidbody = _player.GetComponent<Rigidbody>();
    }
}
