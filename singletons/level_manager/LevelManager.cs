using Godot;
using System;

public partial class LevelManager : Node
{
    [Signal] public delegate void AddLevelEventHandler(Node2D level);

    [Signal] public delegate void ActivateAllLevelsEventHandler();
    [Signal] public delegate void PauseAllLevelsEventHandler();
    [Signal] public delegate void UnpauseAllLevelsEventHandler();

    [Signal] public delegate void SetCurrentLevelEventHandler(string levelName);

    [Signal] public delegate void LevelActivateEventHandler(string levelName);
    [Signal] public delegate void LevelResetEventHandler();
    [Signal] public delegate void LevelPauseEventHandler();
    [Signal] public delegate void LevelUnpauseEventHandler();

    private Node _levelContainer;
    private Node _currLevel;

    public static LevelManager Instance;

    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        _levelContainer = GetTree().Root.GetNode<Node>("LevelContainer");

        AddLevel += OnAddLevel;

        ActivateAllLevels += OnActivateAllLevels;
        PauseAllLevels += OnPauseAllLevels;
        UnpauseAllLevels += OnUnpauseAllLevels;

        SetCurrentLevel += OnSetCurrentLevel;

        LevelActivate += OnLevelActivate;
    }

    private void OnAddLevel(Node2D level)
    {
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


    private void OnSetCurrentLevel(string levelName)
    {
        _currLevel = _levelContainer.GetNode<Node2D>(levelName);
        GetTree().ChangeSceneToFile(_levelContainer.GetNode<Node2D>(levelName).GetPath());
    }


    private void OnLevelActivate(string levelName)
    {
        Node2D level = _levelContainer.GetNode<Node2D>(levelName);
        level.ProcessMode = ProcessModeEnum.Always;
        level.Show();
    }

}
