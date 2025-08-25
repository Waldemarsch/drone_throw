using Godot;
using System;

public partial class MainMenu : Control
{
    private VBoxContainer _MainMenu;
    private PanelContainer _SettingsMenu;
    private TextureButton _SettingsExitButton;

    public override void _Ready()
    {
        _MainMenu = GetNode<VBoxContainer>("MainPanel/MainMenu");
        _MainMenu.GetNode<TextureButton>("StartGameButton").Pressed += OnStartGameButtonPressed;
        _MainMenu.GetNode<TextureButton>("SettingsButton").Pressed += OnSettingsButtonPressed;

        _SettingsMenu = GetNode<PanelContainer>("MainPanel/SettingsMenu");
        _SettingsMenu.GetNode<TextureButton>("MarginContainer/SettingsExitButton").Pressed += OnSettingsExitButtonPressed;
        _SettingsMenu.GetNode<TextureButton>("MarginContainer/VolumeSetting/MinVolume").Pressed += OnSettingsMinVolumeButtonPressed;
        _SettingsMenu.GetNode<TextureButton>("MarginContainer/VolumeSetting/MaxVolume").Pressed += OnSettingsMaxVolumeButtonPressed;
    }

    private async void OnStartGameButtonPressed()
    {
        SceneManager.Instance.EmitSignal(SceneManager.SignalName.Change);

        await ToSignal(SceneManager.Instance, SceneManager.SignalName.ChangeAllowed);

        UIManager.Instance.EmitSignal(UIManager.SignalName.HideUIElement, "MainMenu");
        UIManager.Instance.EmitSignal(UIManager.SignalName.ShowUIElement, "GameInterface");

        LevelManager.Instance.EmitSignal(LevelManager.SignalName.SetCurrentLevel, "MainWorld");
    }

    private void OnSettingsButtonPressed()
    {
        _SettingsMenu.Show();
    }

    private void OnSettingsExitButtonPressed()
    {
        _SettingsMenu.Hide();
    }

    private void OnSettingsMinVolumeButtonPressed()
    {
        _SettingsMenu.GetNode<Slider>("MarginContainer/VolumeSetting/VolumeSlider").Value = 0;
    }

    private void OnSettingsMaxVolumeButtonPressed()
    {
        _SettingsMenu.GetNode<Slider>("MarginContainer/VolumeSetting/VolumeSlider").Value = 100;
    }

}
