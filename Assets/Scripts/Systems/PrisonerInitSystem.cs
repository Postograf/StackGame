using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerInitSystem : IEcsInitSystem
{
    private GameObject _prisoner;
    private HealthBar _healthBar;

    public PrisonerInitSystem(GameObject prisoner, HealthBar healthBar)
    {
        _prisoner = prisoner;
        _healthBar = healthBar;
    }

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var shared = systems.GetShared<SharedData>();
        var prisonerEntity = world.NewEntity();

        shared.AddEntityGameObject(prisonerEntity, _prisoner);

        ref var prisonerComponent = ref world.GetPool<PrisonerComponent>().Add(prisonerEntity);
        prisonerComponent.MaxHealth = shared.Config.MaxHealth;
        prisonerComponent.Health = shared.Config.MaxHealth;
        prisonerComponent.HealthBar = _healthBar;
        prisonerComponent.Animator = _prisoner.GetComponent<Animator>();

        if (_prisoner.TryGetComponent(out PrisonerColliderView prisonerCollider))
        {
            prisonerCollider.World = world;
            prisonerCollider.SharedData = systems.GetShared<SharedData>();
        }
    }
}
