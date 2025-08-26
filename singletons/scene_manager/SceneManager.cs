using Godot;
using System;
using System.Threading.Tasks;
using Godot.Collections;

public partial class SceneManager : Node
{
    public static SceneManager Instance;

    [Export] private Godot.Collections.Dictionary _handlerScenes;

    [Signal] public delegate void LoadEventHandler(Array<string> targetScenesPaths);
    [Signal] public delegate void LoadingStartedEventHandler(Array<string> targetScenesPaths);
    [Signal] public delegate void LoadingCompletedEventHandler();

    [Signal] public delegate void AllowSceneTransitionEventHandler();

    [Signal] public delegate void ChangeEventHandler();
    [Signal] public delegate void ChangeStartedEventHandler();

    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        Load += OnLoad;

        Change += OnChange;
    }



    private void OnLoad(Array<string> targetScenesPaths)
    {
        if ((PackedScene)_handlerScenes["OnLoad Scene"] is PackedScene onLoadScene)
        {
            OnLoadScene onChangeSceneInstance = onLoadScene.Instantiate<OnLoadScene>();
            GetTree().Root.AddChild(onChangeSceneInstance);

            EmitSignal(SignalName.LoadingStarted, targetScenesPaths);
        }
    }

    private void OnChange()
    {
        if ((PackedScene)_handlerScenes["OnChange Scene"] is PackedScene onChangeScene)
        {
            OnChangeScene onChangeSceneInstance = onChangeScene.Instantiate<OnChangeScene>();
            GetTree().Root.AddChild(onChangeSceneInstance);
            EmitSignal(SignalName.ChangeStarted);
        }
    }
}