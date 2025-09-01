using DroneThrow;
using Godot;
using System;
using Godot.Collections;
using System.Linq;

public partial class Spawner : Node
{
    [Export] public Array<PackedScene> PlainsEntities;
    [Export] private Array<PackedScene> _cityEntities;
    [Export] private Array<PackedScene> _skyEntities;
    [Export] private Array<PackedScene> _spaceEntities;

    [Export] private float _spawnDistance = 80000f;
    [Export] private int _spawnCount = 2;
    [Export] private int _maxMobs = 10;
    [Export] private float _spawnTime = 1;

    [Export] float floorY = 43220f;

    private Dictionary<BiomeTypes, Array<PackedScene>> _biomeTypeToEntitiesList = new();

    private Timer _spawnTimer;

    private Array<Entity> _spawnedEntities;

    private PlayerBody _playerBody;

    public override void _Ready()
    {
        base._Ready();

        _spawnedEntities = new();

        _biomeTypeToEntitiesList[BiomeTypes.Plains] = PlainsEntities;
        _biomeTypeToEntitiesList[BiomeTypes.City] = _cityEntities;
        _biomeTypeToEntitiesList[BiomeTypes.Sky] = _skyEntities;
        _biomeTypeToEntitiesList[BiomeTypes.Space] = _spaceEntities;

        _spawnTimer = GetNode<Timer>("Timer");
        _spawnTimer.WaitTime = _spawnTime;
        _spawnTimer.Timeout += OnSpawnTimerTimeout;
        _spawnTimer.Start();
    }

    private void OnSpawnTimerTimeout()
    {
        _playerBody = PlayerManager.Instance._playerBody;
        if (_spawnedEntities.Count != 0)
        {
            var mobsToDespawnResult = _spawnedEntities.Where(mob => mob != null && IsInstanceValid(mob) && Mathf.Abs(mob.GlobalPosition.X - _playerBody.GlobalPosition.X) > _spawnDistance * 1.2);
            if (mobsToDespawnResult != null)
            {
                var mobsToDespawn = mobsToDespawnResult.ToList();

                foreach (var mob in mobsToDespawn)
                {
                    _spawnedEntities.Remove(mob);
                    mob.QueueFree();
                }
            }    
        }

        if (_spawnedEntities.Count < _maxMobs) SpawnMobs();
    }

    private void SpawnMobs()
    {
        var spawnList = _biomeTypeToEntitiesList[PlayerManager.Instance.CurrentBiome];

        for (var i = 0; i < _spawnCount; i++)
        {
            float spawnX = (float)GD.RandRange(_playerBody.GlobalPosition.X + 5000f, _playerBody.GlobalPosition.X + _spawnDistance);

            PackedScene mobToSpawn = (PackedScene)spawnList[GD.RandRange(0, spawnList.Count - 1)].Duplicate();

            Entity spawnedMob = (Entity)mobToSpawn.Instantiate();

            spawnedMob.GlobalPosition = new Vector2(spawnX, floorY - spawnedMob.GetNode<Sprite2D>("Sprite2D").Texture.GetSize().Y / 2 * spawnedMob.Scale.Y);

            _spawnedEntities.Add(spawnedMob);

            AddChild(spawnedMob);
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
    }


}
