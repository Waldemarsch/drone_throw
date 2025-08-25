using Godot;
using System;

public partial class LevelManager : Node
{
    [Signal] public delegate void LoadLevelEventHandler(Node2D level);
    [Signal] public delegate void AddLevelEventHandler(string levelName);

    [Signal] public delegate void ActivateAllLevelsEventHandler();
    [Signal] public delegate void PauseAllLevelsEventHandler();
    [Signal] public delegate void UnpauseAllLevelsEventHandler();
    [Signal] public delegate void ResetAllLevelsEventHandler();

    [Signal] public delegate void LevelActivateEventHandler(string levelName);
    [Signal] public delegate void LevelPauseEventHandler();
    [Signal] public delegate void LevelUnpauseEventHandler();

    private LevelContainer _levelContainer;
    private Node _currLevel;

    public static LevelManager Instance;

    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        _levelContainer = GetTree().Root.GetNode<LevelContainer>("Main/World/LevelContainer");

        LoadLevel += OnLoadLevel;
        AddLevel += OnAddLevel;

        ActivateAllLevels += OnActivateAllLevels;
        PauseAllLevels += OnPauseAllLevels;
        UnpauseAllLevels += OnUnpauseAllLevels;

        LevelActivate += OnLevelActivate;
    }

    private void OnLoadLevel(Node2D level)
    {
        _levelContainer.loadedLevelScenes.Add(level);
    }

    private void OnAddLevel(string levelName)
    {
        Node2D level = null;
        foreach (Node2D loadedLevelScene in _levelContainer.loadedLevelScenes)
        {
            if (loadedLevelScene.Name == levelName)
            {
                level = loadedLevelScene;
                break;
            }
        }
        if (level == null) return;
        level.Hide();
        level.ProcessMode = ProcessModeEnum.Disabled;
        _levelContainer.AddChild(level);
    }


    private void OnActivateAllLevels()
    {
        foreach (Node2D level in _levelContainer.GetChildren())
        {
            level.Show();
            level.ProcessMode = ProcessModeEnum.Always;
        }
    }
    private void OnPauseAllLevels()
    {
        foreach (Node2D level in _levelContainer.GetChildren())
        {
            level.ProcessMode = ProcessModeEnum.Disabled;
        }
    }
    private void OnUnpauseAllLevels()
    {
        foreach (Node2D level in _levelContainer.GetChildren())
        {
            level.ProcessMode = ProcessModeEnum.Always;
        }
    }


    private void OnLevelActivate(string levelName)
    {
        Node2D level = _levelContainer.GetNode<Node2D>(levelName);
        level.ProcessMode = ProcessModeEnum.Always;
        level.Show();
    }

}
