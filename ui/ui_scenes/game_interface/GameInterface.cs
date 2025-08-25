using Godot;
using System;

public partial class GameInterface : Control
{
    private PanelContainer _Toolbar;
    private PanelContainer _PauseMenu;
    private Panel _PausedBackground;

    public override void _Ready()
    {
        base._Ready();

        _Toolbar = GetNode<PanelContainer>("Toolbar");
        _Toolbar.GetNode<TextureButton>("MarginContainer/CenterContainer/HBoxContainer/PauseButton").Pressed += () =>
        {
            LevelManager.Instance.EmitSignal(LevelManager.SignalName.PauseAllLevels);
            _PausedBackground.Show();
            _PauseMenu.Show();
        };
        _Toolbar.GetNode<TextureButton>("MarginContainer/CenterContainer/HBoxContainer/ReplayButton").Pressed += () =>
        {
            LevelManager.Instance.EmitSignal(LevelManager.SignalName.ResetAllLevels);
        };

        _PauseMenu = GetNode<PanelContainer>("PauseMenu");
        _PauseMenu.GetNode<TextureButton>("MarginContainer/PauseExitButton").Pressed += () =>
        {
            LevelManager.Instance.EmitSignal(LevelManager.SignalName.UnpauseAllLevels);
            _PausedBackground.Hide();
            _PauseMenu.Hide();
        };
        _PauseMenu.GetNode<TextureButton>("MarginContainer/VolumeSetting/MinVolume").Pressed += () =>
        {
            _PauseMenu.GetNode<Slider>("MarginContainer/VolumeSetting/VolumeSlider").Value = 0;
        };
        _PauseMenu.GetNode<TextureButton>("MarginContainer/VolumeSetting/MaxVolume").Pressed += () =>
        {
            _PauseMenu.GetNode<Slider>("MarginContainer/VolumeSetting/VolumeSlider").Value = 100;
        };
        

        _PausedBackground = GetNode<Panel>("PausedBackground");
    }

}
