using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPrisonersSystem : IEcsRunSystem
{
    private GameObject _gameOverScreen;
    private string _deathAnimation = "Death_b";

    public HurtPrisonersSystem(GameObject gameOverScreen)
    {
        _gameOverScreen = gameOverScreen;
    }

    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var shared = systems.GetShared<SharedData>();

        var hurt = shared.Config.LossHealthPerSecond * Time.deltaTime;
        var prisoners = world.Filter<PrisonerComponent>().End();
        var prisonersPool = world.GetPool<PrisonerComponent>();

        foreach (var prisonerEntity in prisoners)
        {
            ref var prisoner = ref prisonersPool.Get(prisonerEntity);
            prisoner.DelayForFood -= Time.deltaTime;
            prisoner.Health -= hurt;
            prisoner.HealthBar.Filling = prisoner.Health / prisoner.MaxHealth;

            if (prisoner.Health <= 0)
            {
                prisoner.Animator?.SetBool(_deathAnimation, true);
                _gameOverScreen.SetActive(true);
            }
        }
    }
}
