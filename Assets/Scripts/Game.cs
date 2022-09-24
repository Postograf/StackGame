using Leopotam.EcsLite;

using System.Collections.Generic;

using UnityEngine;

sealed class Game : MonoBehaviour 
{
    [SerializeField] private Config _configuration;
    [SerializeField] private GameObject _gameOverScreen;

    [Header("Player")]
    [SerializeField] private GameObject _player;
    [SerializeField] private FoodStackUI _foodStackUI;

    [Header("Prisoner")]
    [SerializeField] private GameObject _prisoner;
    [SerializeField] private HealthBar _healthBar;

    [Header("Area")]
    [SerializeField] private GameObject[] _walls;
    [SerializeField] private Vector3 _spawnerPosition;

    private IEcsSystems _updateSystems;
    private IEcsSystems _fixedUpdateSystems;
    private IEcsSystems _initSystems;
    private SharedData _data;
    
    public EcsWorld World { get; private set; }

    private void Awake()
    {
        World = new EcsWorld();

        _data = new SharedData(World, _configuration);
        var spawnerObject = new GameObject();
        spawnerObject.transform.position = _spawnerPosition;
        var spawner = spawnerObject.AddComponent<FoodSpawner>();

        _initSystems = new EcsSystems(World, _data);
        _initSystems
            .Add(new PlayerInitSystem(_player, _foodStackUI))
            .Add(new PrisonerInitSystem(_prisoner, _healthBar))
            .Add(new WallsInitSystem(_walls))
#if UNITY_EDITOR
            .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
            .Init();

        _updateSystems = new EcsSystems(World, _data);
        _updateSystems
            .Add(new PlayerFoodCollisionSystem())
            .Add(new PrisonerPlayerCollisionsSystem())
            .Add(new InputMovementSystem())
            .Add(new ReturnToPoolFoodSystem(spawner))
            .Add(new HurtPrisonersSystem(_gameOverScreen))
            .Init();

        _fixedUpdateSystems = new EcsSystems(World, _data);
        _fixedUpdateSystems
            .Init();

        spawner.Init(World, _data);
    }

    private void Update() 
    {
        _updateSystems?.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems?.Run();
    }

    private void OnDestroy() 
    {
        _initSystems?.Destroy();
        _updateSystems?.Destroy();
        _fixedUpdateSystems?.Destroy();
        World?.Destroy();
    }
}