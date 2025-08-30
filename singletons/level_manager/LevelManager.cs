using Godot;
using System;

public partial class LevelManager : Node
{
    [Signal] public delegate void LoadLevelEventHandler(Node2D level);
    [Signal] private delegate void LoadLevelFinishedEventHandler();

    [Signal] public delegate void ChangeLevelEventHandler(string levelName, string spawnPointName);
    [Signal] public delegate void ActivateLevelEventHandler();
    [Signal] public delegate void PauseLevelEventHandler();
    [Signal] public delegate void UnpauseLevelEventHandler();
    [Signal] public delegate void ResetLevelEventHandler();

    [Export] public float GravityForce = 100f;

    private LevelContainer _levelContainer;
    private Node2D _currLevel;

    public static LevelManager Instance;

    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        _levelContainer = GetTree().Root.GetNode<LevelContainer>("Main/World/LevelContainer");

        LoadLevel += OnLoadLevel;

        ChangeLevel += OnChangeLevel;
        ActivateLevel += OnActivateLevel;
        PauseLevel += OnPauseLevel;
        UnpauseLevel += OnUnpauseLevel;
        ResetLevel += OnResetLevel;
    }

    private void OnLoadLevel(Node2D level)
    {
        if (level == null) return;

        _levelContainer.loadedLevel = (Node2D)level.Duplicate();

        level.Hide();
        level.ProcessMode = ProcessModeEnum.Disabled;
        _levelContainer.AddChild(level);

        _currLevel = level;

        EmitSignal(SignalName.LoadLevelFinished);
    }


    private async void OnChangeLevel(string levelName, string spawnPointName)
    {
        if (_currLevel.Name == levelName)
        {
            EmitSignal(SignalName.ActivateLevel);
        }

        else
        {
            SceneManager.Instance.EmitSignal(SceneManager.SignalName.Load, _levelContainer.levelContainerResource.Paths[levelName]);
            SceneManager.Instance.AllowSceneTransition += () => _currLevel.QueueFree();

            await ToSignal(this, SignalName.LoadLevelFinished);

            _currLevel = _levelContainer.GetNode<Node2D>(levelName);
            EmitSignal(SignalName.ActivateLevel);
        }

        PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.TransitPlayerBody, _currLevel, spawnPointName);
    }

    private void OnActivateLevel()
    {
        _currLevel.ProcessMode = ProcessModeEnum.Always;
        _currLevel.Show();
    }

    private void OnPauseLevel()
    {
        _currLevel.ProcessMode = ProcessModeEnum.Disabled;
    }

    private void OnUnpauseLevel()
    {
        _currLevel.ProcessMode = ProcessModeEnum.Always;
    }

    private void OnResetLevel()
    {
        _currLevel.QueueFree();
        _currLevel = (Node2D)_levelContainer.loadedLevel.Duplicate();
        _levelContainer.AddChild(_currLevel);

        PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.TransitPlayerBody, _currLevel, "DefaultSpawn");
    }
}
