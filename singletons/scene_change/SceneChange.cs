using Godot;
using System;
using System.Reflection;
using System.Threading.Tasks;

public partial class SceneChange : CanvasLayer
{
    [Signal]
    public delegate void LoadingFinishedEventHandler();

    private ColorRect ColorRectNode;
    private AnimationPlayer AnimationPlayerNode;
    private TextureProgressBar ProgressBarNode;
    public string TargetScene;
    private bool is_loaded = false;

    public override void _Ready()
    {
        ColorRectNode = GetNode<ColorRect>("ColorRect");
        AnimationPlayerNode = GetNode<AnimationPlayer>("AnimationPlayer");
        ProgressBarNode = GetNode<TextureProgressBar>("ColorRect/CenterContainer/VBoxContainer/TextureProgressBar");
        ProcessMode = ProcessModeEnum.Disabled;
    }
    public override void _Process(double delta)
    {
        if (!is_loaded)
        {Godot.Collections.Array progress = [];
        
        var status = ResourceLoader.LoadThreadedGetStatus(TargetScene, progress);
        
        switch (status)
        {
            case ResourceLoader.ThreadLoadStatus.InProgress:
                ProgressBarNode.Value = (float)progress[0] * 100;
                break;
            case ResourceLoader.ThreadLoadStatus.Loaded:
                is_loaded = true;
                ProgressBarNode.Value = 100;
                EmitSignal(SignalName.LoadingFinished);
                break;
        }}
    }
    public async Task LoadScene(string scenePath)
    {
        ProcessMode = ProcessModeEnum.Always;
        TargetScene = scenePath;
        AnimationPlayerNode.Active = true;
        AnimationPlayerNode.Play("Dissolve");
        await ToSignal(AnimationPlayerNode, AnimationPlayer.SignalName.AnimationFinished);
        ResourceLoader.LoadThreadedRequest(TargetScene);
        ProcessMode = ProcessModeEnum.Always;

        await ToSignal(this, SignalName.LoadingFinished);

        var nextScene = ResourceLoader.LoadThreadedGet(TargetScene);

        AnimationPlayerNode.PlayBackwards("Dissolve");
        await ToSignal(AnimationPlayerNode, AnimationPlayer.SignalName.AnimationFinished);

        GetTree().ChangeSceneToPacked((PackedScene)nextScene);
        QueueFree();
    }
    
}
