using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedData
{
    private Dictionary<GameObject, int> _entityByObject;
    private EcsWorld _world;
    
    public Config Config { get; private set; }

    public SharedData(EcsWorld world, Config config)
    {
        _world = world;
        Config = config;

        _entityByObject = new Dictionary<GameObject, int>();
    }

    public void AddEntityGameObject(int entity, GameObject gameObject)
    {
        var gameObjectsPool = _world.GetPool<GameObjectComponent>();

        if (gameObjectsPool.Has(entity) == false)
        {
            ref var gameObjectComponent = ref gameObjectsPool.Add(entity);
            gameObjectComponent.GameObject = gameObject;
        }

        _entityByObject[gameObject] = entity;
    }

    public void RemoveGameObject(GameObject gameObject)
    {
        _entityByObject.Remove(gameObject);
    }

    public int GetEntity(GameObject gameObject)
    {
        if (_entityByObject.ContainsKey(gameObject))
            return _entityByObject[gameObject];
        return -1;
    }
}
