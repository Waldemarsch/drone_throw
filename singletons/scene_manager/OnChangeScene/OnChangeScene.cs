using Godot;
using System;
using System.Diagnostics;

public partial class OnChangeScene : CanvasLayer
{
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        base._Ready();

        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        SceneManager.Instance.ChangeStarted += OnChangeStarted;

        ProcessMode = ProcessModeEnum.Disabled;

    }

    private async void OnChangeStarted()
    {
        ProcessMode = ProcessModeEnum.Always;

        _animationPlayer.Play("Dissolve");
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);

        SceneManager.Instance.EmitSignal(SceneManager.SignalName.AllowSceneTransition);

        _animationPlayer.PlayBackwards("Dissolve");
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);

        QueueFree();
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        SceneManager.Instance.ChangeStarted -= OnChangeStarted;
    }


}
