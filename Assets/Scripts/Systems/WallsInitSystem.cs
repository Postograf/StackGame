using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsInitSystem : IEcsInitSystem
{
    private GameObject[] _walls;

    public WallsInitSystem(GameObject[] walls)
    {
        _walls = walls;
    }

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        foreach (var wall in _walls)
        {
            var wallEntity = world.NewEntity();

            systems.GetShared<SharedData>().AddEntityGameObject(wallEntity, wall);

            if (wall.TryGetComponent(out WallColliderView wallCollider))
            {
                wallCollider.World = world;
                wallCollider.SharedData = systems.GetShared<SharedData>();
            }
        }
    }
}
