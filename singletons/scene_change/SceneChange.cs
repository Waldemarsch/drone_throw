using Godot;
using System;
using System.Reflection;
using System.Threading.Tasks;

public partial class SceneChange : CanvasLayer
{
    private ColorRect ColorRectNode;
    private AnimationPlayer AnimationPlayerNode;

    public override void _Ready()
    {
        ColorRectNode = GetNode<ColorRect>("ColorRect");
        AnimationPlayerNode = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public async Task ChangeScene(string nodePath)
    {
        AnimationPlayerNode.Play("Dissolve");
        await ToSignal(AnimationPlayerNode, AnimationPlayer.SignalName.AnimationFinished);
        GetTree().ChangeSceneToFile(nodePath);
        AnimationPlayerNode.PlayBackwards("Dissolve");
        await ToSignal(AnimationPlayerNode, AnimationPlayer.SignalName.AnimationFinished);
        this.QueueFree();
    }

}
