using Godot;
using System;

public partial class MainMenuUi : Control
{
    private TextureButton _startGameButton;
    private TextureButton _settingsButton;

    public override void _Ready()
    {
        _startGameButton = GetNode<TextureButton>("MainPanel/MainMenu/StartGameButton");
        _startGameButton.Pressed += OnStartGameButtonPressed;
    }

    private void OnStartGameButtonPressed()
    {
        // EmitSignal(SceneManager.SignalName.Change, "Main World");
    }

}
