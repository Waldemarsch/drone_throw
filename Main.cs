using Godot;
using System;

public partial class Main : Node
{

    [Export] public StartGameResource startGameResource;

    public override void _Ready()
    {
        CallDeferred(nameof(LoadGame));
    }
    private void LoadGame()
    {
        SceneManager.Instance.EmitSignal(SceneManager.SignalName.Load, startGameResource.StartScenesArray);

        SceneManager.Instance.LoadingFinished += _ => { GetTree().Root.GetNode<Control>("Ui/MainMenuUI").Show(); QueueFree();};   
    }

}
