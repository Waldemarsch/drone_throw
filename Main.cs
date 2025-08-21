using Godot;
using System;

public partial class Main : Node
{

    public override void _Ready()
    {
        CallDeferred(nameof(LoadGame));
    }
    private void LoadGame()
    {
        GameManager.Instance.ChangeSceneTo("res://scenes/main_menu.tscn");
        QueueFree();
    }

}
