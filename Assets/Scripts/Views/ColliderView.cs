using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderView : MonoBehaviour
{
    public EcsWorld World { get; set; }
    public SharedData SharedData { get; set; }
}
