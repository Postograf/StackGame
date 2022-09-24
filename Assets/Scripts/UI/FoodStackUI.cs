using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStackUI : MonoBehaviour
{
    [SerializeField] private FoodUI _foodPrefab;

    private Stack<FoodUI> _foodStack;

    private void Awake()
    {
        _foodStack = new Stack<FoodUI>();
    }

    public void Push(float value)
    {
        var food = Instantiate(_foodPrefab, transform);
        food.Value = value;
        _foodStack.Push(food);
    }

    public void Pop()
    {
        Destroy(_foodStack.Pop().gameObject);
    }
}
