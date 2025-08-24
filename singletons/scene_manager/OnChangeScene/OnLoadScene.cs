using Godot;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Godot.Collections;
using System.Linq;

public partial class OnLoadScene : CanvasLayer
{
    [Signal]
    private delegate void LoadingFinishedEventHandler();

    private ColorRect ColorRectNode;

    private AnimationPlayer AnimationPlayerNode;

    private TextureProgressBar ProgressBarNode;

    public Array<string> TargetScenesPaths;

    private bool is_loaded = false;
    private float loading_status;

    private bool mayProceedAnim = false;

    public override void _Ready()
    {
        ColorRectNode = GetNode<ColorRect>("ColorRect");
        AnimationPlayerNode = GetNode<AnimationPlayer>("AnimationPlayer");
        ProgressBarNode = GetNode<TextureProgressBar>("ColorRect/CenterContainer/VBoxContainer/TextureProgressBar");
        ProcessMode = ProcessModeEnum.Disabled;

        SceneManager.Instance.LoadingStarted += OnLoadingStarted;
        LoadingFinished += OnLoadingFinished;
    }
    public override void _Process(double delta)
    {
        if (is_loaded || !mayProceedAnim) return;

        loading_status = 0.0f;

        foreach (var _targetScenePath in TargetScenesPaths)
        {
            Godot.Collections.Array progress = [];
            var status = ResourceLoader.LoadThreadedGetStatus(_targetScenePath, progress);
            loading_status += (float)progress[0];
        }

        switch (loading_status)
        {
            case < 1.0f:
                ProgressBarNode.Value = loading_status * 100;
                break;
            case 1.0f:
                is_loaded = true;
                ProgressBarNode.Value = 100;
                EmitSignal(SignalName.LoadingFinished);
                break;
        }
    }
    
    public void OnLoadingStarted(Array<string> targetScenesPaths)
    {
        TargetScenesPaths = targetScenesPaths;
        ProcessMode = ProcessModeEnum.Always;
        AnimationPlayerNode.Active = true;

        _ = PlayDissolveAnimation();

    }

    private async Task PlayDissolveAnimation()
    {
        AnimationPlayerNode.Play("Dissolve");

        await ToSignal(AnimationPlayerNode, AnimationPlayer.SignalName.AnimationFinished);

        foreach (var _targetScenePath in TargetScenesPaths)
        {
            ResourceLoader.LoadThreadedRequest(_targetScenePath);
        }

        mayProceedAnim = true;

    }

    private void PlayReverseDissolveAnimation()
    {
        AnimationPlayerNode.PlayBackwards("Dissolve");
        AnimationPlayerNode.AnimationFinished += _ => { QueueFree(); };
    }

    public void OnLoadingFinished()
    {
        Array<PackedScene> loadedScenes = [];

        foreach (var _targetScenePath in TargetScenesPaths)
        {
            loadedScenes.Add((PackedScene)ResourceLoader.LoadThreadedGet(_targetScenePath));
        }

        PlayReverseDissolveAnimation();

        SceneManager.Instance.EmitSignal(SceneManager.SignalName.LoadingFinished, loadedScenes);
    }
    
}
