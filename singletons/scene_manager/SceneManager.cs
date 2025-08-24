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
    [Signal] public delegate void LoadingFinishedEventHandler(Array<PackedScene> loadedScenes);
    [Signal] public delegate void LoadingCompletedEventHandler();

    private Node LevelContainer;

    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        Load += OnLoad;
        LoadingFinished += OnLoadingFinished;

        LevelContainer = GetTree().Root.GetNode<Node>("LevelContainer");
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

    private void OnLoadingFinished(Array<PackedScene> loadedScenes)
    {
        foreach (var loadedScene in loadedScenes)
        {
            var loadedSceneInstance = loadedScene.Instantiate();

            if (loadedSceneInstance is Control)
            {
                UIManager.Instance.EmitSignal(UIManager.SignalName.AddUIElement, loadedSceneInstance);
            }

            else if (loadedSceneInstance is Node2D)
            {
                LevelContainer.AddChild(loadedSceneInstance);
            }
        }
        EmitSignal(SignalName.LoadingCompleted);
    }
}