using Godot;
using System;

public partial class MainMenuUi : CanvasLayer
{
    private TextureButton _startGameButton;
    private TextureButton _settingsButton;

    public override void _Ready()
    {
        _startGameButton = GetNode<TextureButton>("MainPanel/MainMenu/StartGameButton");
        _startGameButton.Pressed += () => GameManager.Instance.ChangeSceneTo("res://scenes/main_world.tscn");
    }

}
