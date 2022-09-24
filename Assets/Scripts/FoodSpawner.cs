using Leopotam.EcsLite;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private EcsWorld _world;
    private Config _config;
    private SharedData _data;
    private Coroutine _coroutine;
    private List<Food> _pool;
    
    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    public void Init(EcsWorld world, SharedData shared)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            foreach (var pooledObject in _pool)
            {
                Destroy(pooledObject.GameObject);
            }
        }

        _pool = new List<Food>();
        _world = world;
        _config = shared.Config;
        _data = shared;

        _coroutine = StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        var topRight = _config.SpawnAreaTopRightCorner;
        var bottomLeft = _config.SpawnAreaBottomLeftCorner;
        var position = new Vector3();

        while (true)
        {
            yield return new WaitForSeconds(_config.SpawnDelay);

            var food = GetFood();
            position.x = Random.Range(bottomLeft.x, topRight.x);
            position.y = Random.Range(bottomLeft.y, topRight.y);
            position.z = Random.Range(bottomLeft.z, topRight.z);
            food.GameObject.transform.position = position;
            AddObjectToEcs(food);
        }
    }

    public void AddObjectToEcs(Food food)
    {
        var entity = _world.NewEntity();

        var gameObject = food.GameObject;
        _data.AddEntityGameObject(entity, gameObject);

        ref var movableComponent = ref _world.GetPool<MovableComponent>().Add(entity);
        movableComponent.Transform = gameObject.transform;
        movableComponent.Rigidbody = gameObject.GetComponent<Rigidbody>();

        ref var foodComponent = ref _world.GetPool<FoodComponent>().Add(entity);
        var offset = Quaternion.Euler(0, Random.Range(0, 360), 0);
        foodComponent.RecoverableHealth = food.RecoverableHealth;
        foodComponent.InputDirectionOffset = offset;
        foodComponent.Speed = Random.Range(_config.MinSpeed, _config.MaxSpeed);
    }

    public Food GetFood()
    {
        var res = _pool?.FirstOrDefault(x => x.GameObject.activeSelf == false);

        var spawnables = _config.FoodPrefabs;
        if (res == null && spawnables != null && spawnables.Count > 0)
        {
            var prefab = spawnables[Random.Range(0, spawnables.Count)];
            res = new Food(Instantiate(prefab.GameObject, Transform), prefab.RecoverableHealth);
            _pool.Add(res);
        }

        res.GameObject.SetActive(true);
        return res;
    }

    public void ReturnFood(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = Transform.position;
    }
}
