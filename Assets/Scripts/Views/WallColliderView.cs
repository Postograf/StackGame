using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColliderView : ColliderView
{
    private void OnTriggerEnter(Collider other)
    {
        var food = SharedData.GetEntity(other.gameObject);
        if (World.GetPool<FoodComponent>().Has(food))
        {
            World.GetPool<ReturnToPoolFoodComponent>().Add(food);
        }
    }
}
