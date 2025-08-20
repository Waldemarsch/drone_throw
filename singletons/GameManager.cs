using Godot;
using System;

public partial class GameManager : Node
{
    [Export] public PackedScene SceneChangerPackedScene;

    public static GameManager Instance { get; private set; }


    public override void _Ready()
    {
        Instance = this;
    }

    public void ChangeSceneTo(string TargetScenePath)
    {
        SceneChange SceneChangeScene = SceneChangerPackedScene.Instantiate<SceneChange>();
        GetTree().Root.AddChild(SceneChangeScene);
        SceneChangeScene.ChangeScene(TargetScenePath);
    }

}
