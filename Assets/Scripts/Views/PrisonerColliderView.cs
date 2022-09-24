using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PrisonerColliderView : ColliderView
{
    private Dictionary<GameObject, int> _collisonByObject;

    private void Awake()
    {
        _collisonByObject = new Dictionary<GameObject, int>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = SharedData.GetEntity(other.gameObject);
        if (World.GetPool<PlayerComponent>().Has(player))
        {
            var collision = World.NewEntity();
            ref var collisionComponent = ref World.GetPool<PrisonerPlayerCollision>().Add(collision);
            collisionComponent.Player = player;
            collisionComponent.Prisoner = SharedData.GetEntity(gameObject);
            _collisonByObject[other.gameObject] = collision;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var gameObject = other.gameObject;
        if (_collisonByObject.ContainsKey(other.gameObject))
        {
            World.DelEntity(_collisonByObject[gameObject]);
            _collisonByObject.Remove(gameObject);
        }
    }
}
