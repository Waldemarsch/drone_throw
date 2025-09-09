using Godot;
using System;

public partial class GameInterface : Control
{
    private AnimationPlayer _animationPlayer;

    private PanelContainer _Toolbar;
    private PanelContainer _PauseMenu;
    private Panel _PausedBackground;

    private PanelContainer _moneyPanel;

    public override void _Ready()
    {
        base._Ready();

        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        PlayerManager.Instance.PlayerStateChanged += OnPlayerStateChanged;

        _Toolbar = GetNode<PanelContainer>("Toolbar");
        _Toolbar.GetNode<TextureButton>("MarginContainer/CenterContainer/HBoxContainer/PauseButton").Pressed += () =>
        {
            LevelManager.Instance.EmitSignal(LevelManager.SignalName.PauseLevel);
            _PausedBackground.Show();
            _PauseMenu.Show();
        };
        _Toolbar.GetNode<TextureButton>("MarginContainer/CenterContainer/HBoxContainer/ReplayButton").Pressed += () =>
        {
            LevelManager.Instance.EmitSignal(LevelManager.SignalName.ResetLevel);
        };
        _moneyPanel = _Toolbar.GetNode<PanelContainer>("MarginContainer/CenterContainer/HBoxContainer/MoneyPanel");
        _moneyPanel.GetNode<Label>("HBoxContainer/Label").Text = PlayerManager.Instance.PlayerDataResource.Score.ToString();
        PlayerManager.Instance.ScoreChanged += () =>
        {
            _moneyPanel.GetNode<Label>("HBoxContainer/Label").Text = PlayerManager.Instance.PlayerDataResource.Score.ToString();
        };


        _PauseMenu = GetNode<PanelContainer>("PauseMenu");
        _PauseMenu.GetNode<TextureButton>("MarginContainer/PauseExitButton").Pressed += () =>
        {
            LevelManager.Instance.EmitSignal(LevelManager.SignalName.UnpauseLevel);
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

    private void OnPlayerStateChanged(EStateType stateType)
    {
        switch (stateType)
        {
            case EStateType.Idle:
                GetNode<TextureRect>("IdleStateUI").Show();
                _animationPlayer.Play("Idle");
                break;

            case EStateType.BeginRotate:
                GetNode<TextureRect>("IdleStateUI").Hide();
                _animationPlayer.Stop();
                break;
        }
    }

}
