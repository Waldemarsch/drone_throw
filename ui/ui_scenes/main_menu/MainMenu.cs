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
        _MainMenu.GetNode<TextureButton>("SettingsButton").Pressed += () =>
        {
            SoundManager.Instance.EmitSignal(SoundManager.SignalName.PlaySound, (int)ESoundType.Click);
            _SettingsMenu.Show();
        };

        _SettingsMenu = GetNode<PanelContainer>("MainPanel/SettingsMenu");
        _SettingsMenu.GetNode<Slider>("MarginContainer/VolumeSetting/VolumeSlider").ValueChanged += (double value) => {
            SoundManager.Instance.EmitSignal(SoundManager.SignalName.SetMasterVolume, (float)value);
        };
        _SettingsMenu.GetNode<TextureButton>("MarginContainer/SettingsExitButton").Pressed += () =>
        {
            SoundManager.Instance.EmitSignal(SoundManager.SignalName.PlaySound, (int)ESoundType.Click);
            _SettingsMenu.Hide();
        };
        _SettingsMenu.GetNode<TextureButton>("MarginContainer/VolumeSetting/MinVolume").Pressed += () =>
        {
            SoundManager.Instance.EmitSignal(SoundManager.SignalName.PlaySound, (int)ESoundType.Click);
            _SettingsMenu.GetNode<Slider>("MarginContainer/VolumeSetting/VolumeSlider").Value = 0;
        };
        _SettingsMenu.GetNode<TextureButton>("MarginContainer/VolumeSetting/MaxVolume").Pressed += () =>
        {
            SoundManager.Instance.EmitSignal(SoundManager.SignalName.PlaySound, (int)ESoundType.Click);
            _SettingsMenu.GetNode<Slider>("MarginContainer/VolumeSetting/VolumeSlider").Value = 100;
        };
    }

    private async void OnStartGameButtonPressed()
    {
        SoundManager.Instance.EmitSignal(SoundManager.SignalName.PlaySound, (int)ESoundType.Click);
        SoundManager.Instance.EmitSignal(SoundManager.SignalName.StopMusic);

        PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.CreatePlayerData);

        SceneManager.Instance.EmitSignal(SceneManager.SignalName.Change);

        await ToSignal(SceneManager.Instance, SceneManager.SignalName.AllowSceneTransition);

        UIManager.Instance.EmitSignal(UIManager.SignalName.HideUIElement, "MainMenu");
        UIManager.Instance.EmitSignal(UIManager.SignalName.EnableUIElement, "GameInterface");

        LevelManager.Instance.EmitSignal(LevelManager.SignalName.ChangeLevel, "MainWorld", "DefaultSpawn");

        SoundManager.Instance.EmitSignal(SoundManager.SignalName.StartMusic, (int)EMusicType.Main);

        QueueFree();
    }

}
