using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Joystick))]
public class JoystickView : MonoBehaviour
{
    private Joystick _joystick;
    private EcsWorld _world;
    private int _entity;

    private void Start()
    {
        _world = FindObjectOfType<Game>().World;
        _entity = _world.NewEntity();
        _world.GetPool<InputMovementComponent>().Add(_entity);

        _joystick = GetComponent<Joystick>();
        _joystick.DirectionChanged += OnDirectionChanged;
    }

    private void OnDirectionChanged(Vector3 direction)
    {
        ref var movement = ref _world.GetPool<InputMovementComponent>().Get(_entity);
        movement.Direction = direction;
    }
}
