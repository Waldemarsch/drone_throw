using Godot;
using System;

public partial class UpgradeMenu : Control
{
    [Export] public StyleBoxFlat BoughtUpgradeStyleBox;
    [Export] public StyleBoxFlat UnboughtUpgradeStyleBox;

    private TextureButton _playButton;

    private VBoxContainer _upgradeLevelContainer;

    private PanelContainer _moneyPanel;

    private VBoxContainer _upgradePricesContainer;

    private VBoxContainer _upgradeButtonsContainer;

    public override void _Ready()
    {
        base._Ready();

        _playButton = GetNode<TextureButton>("PanelContainer/PlayButton");
        _playButton.Pressed += OnPlayButtonPressed;

        _upgradeLevelContainer = GetNode<VBoxContainer>("PanelContainer/HBoxContainer/UpgradeLevels");
        foreach (var upgradeLevel in _upgradeLevelContainer.GetChildren())
        {
            int intUpgradeLevel = 0;
            if (upgradeLevel.Name == "GeneralUpgradeBox") intUpgradeLevel = PlayerManager.Instance.PlayerDataResource.GeneralUpgrade.CurrentUpgradeLevel;
            else if (upgradeLevel.Name == "EngineUpgradeBox") intUpgradeLevel = PlayerManager.Instance.PlayerDataResource.EngineUpgrade.CurrentUpgradeLevel;
            else if (upgradeLevel.Name == "GunUpgradeBox") intUpgradeLevel = PlayerManager.Instance.PlayerDataResource.GunUpgrade.CurrentUpgradeLevel;
            else if (upgradeLevel.Name == "GearUpgradeBox") intUpgradeLevel = PlayerManager.Instance.PlayerDataResource.GearUpgrade.CurrentUpgradeLevel;
            else if (upgradeLevel.Name == "ShieldUpgradeBox") intUpgradeLevel = PlayerManager.Instance.PlayerDataResource.ShieldUpgrade.CurrentUpgradeLevel;

            var panelContainer = upgradeLevel.GetNode<HBoxContainer>("HBoxContainer");

            for (var i = 0; i < intUpgradeLevel; i++)
            {
                panelContainer.GetChild<PanelContainer>(i).AddThemeStyleboxOverride("panel", BoughtUpgradeStyleBox);
            }
        }
        PlayerManager.Instance.UpgradeApplied += (EUpgradeType upgradeType) =>
        {
            UpgradeData upgradeInfo = null;
            if (upgradeType == EUpgradeType.General) upgradeInfo = PlayerManager.Instance.PlayerDataResource.GeneralUpgrade;
            else if (upgradeType == EUpgradeType.Engine) upgradeInfo = PlayerManager.Instance.PlayerDataResource.EngineUpgrade;
            else if (upgradeType == EUpgradeType.Gun) upgradeInfo = PlayerManager.Instance.PlayerDataResource.GunUpgrade;
            else if (upgradeType == EUpgradeType.Gear) upgradeInfo = PlayerManager.Instance.PlayerDataResource.GearUpgrade;
            else if (upgradeType == EUpgradeType.Shield) upgradeInfo = PlayerManager.Instance.PlayerDataResource.ShieldUpgrade;

            var upgradeLevel = _upgradeLevelContainer.GetChild((int)upgradeType);
            var panelContainer = upgradeLevel.GetNode<HBoxContainer>("HBoxContainer");

            for (var i = 0; i < upgradeInfo.CurrentUpgradeLevel; i++)
            {
                panelContainer.GetChild<PanelContainer>(i).AddThemeStyleboxOverride("panel", BoughtUpgradeStyleBox);
            }
        };



        _moneyPanel = GetNode<PanelContainer>("PanelContainer/MoneyPanelControl/MoneyPanel");
        _moneyPanel.GetNode<Label>("HBoxContainer/Label").Text = PlayerManager.Instance.PlayerDataResource.Score.ToString();
        PlayerManager.Instance.ScoreChanged += () =>
        {
            _moneyPanel.GetNode<Label>("HBoxContainer/Label").Text = PlayerManager.Instance.PlayerDataResource.Score.ToString();
        };

        _upgradePricesContainer = GetNode<VBoxContainer>("PanelContainer/HBoxContainer/UpgradePrices");
        foreach (var upgradePrice in _upgradePricesContainer.GetChildren())
        {
            var label = upgradePrice.GetNode<Label>("Label");
            if (upgradePrice.Name == "General")
                label.Text = PlayerManager.Instance.PlayerDataResource.GeneralUpgrade.GetCurrentUpgradePrice().ToString();
            else if (upgradePrice.Name == "Engine")
                label.Text = PlayerManager.Instance.PlayerDataResource.EngineUpgrade.GetCurrentUpgradePrice().ToString();
            else if (upgradePrice.Name == "Gun")
                label.Text = PlayerManager.Instance.PlayerDataResource.GunUpgrade.GetCurrentUpgradePrice().ToString();
            else if (upgradePrice.Name == "Gear")
                label.Text = PlayerManager.Instance.PlayerDataResource.GearUpgrade.GetCurrentUpgradePrice().ToString();
            else if (upgradePrice.Name == "Shield")
                label.Text = PlayerManager.Instance.PlayerDataResource.ShieldUpgrade.GetCurrentUpgradePrice().ToString();
        }
        PlayerManager.Instance.UpgradeApplied += (EUpgradeType upgradeType) =>
        {
            
            string text = "";
            UpgradeData upgradeInfo = null;
            if (upgradeType == EUpgradeType.General) upgradeInfo = PlayerManager.Instance.PlayerDataResource.GeneralUpgrade;
            else if (upgradeType == EUpgradeType.Engine) upgradeInfo = PlayerManager.Instance.PlayerDataResource.EngineUpgrade;
            else if (upgradeType == EUpgradeType.Gun) upgradeInfo = PlayerManager.Instance.PlayerDataResource.GunUpgrade;
            else if (upgradeType == EUpgradeType.Gear) upgradeInfo = PlayerManager.Instance.PlayerDataResource.GearUpgrade;
            else if (upgradeType == EUpgradeType.Shield) upgradeInfo = PlayerManager.Instance.PlayerDataResource.ShieldUpgrade;

            if (upgradeInfo.CurrentUpgradeLevel == 4) text = "MAX";

            else text = upgradeInfo.GetCurrentUpgradePrice().ToString();

            _upgradePricesContainer.GetChildren()[(int)upgradeType].GetNode<Label>("Label").Text = text;
        };

        _upgradeButtonsContainer = GetNode<VBoxContainer>("PanelContainer/HBoxContainer/UpgradeButtons");
        _upgradeButtonsContainer.GetNode<TextureButton>("UpgradeGeneralButton").Pressed += () =>
        {
            if (PlayerManager.Instance.PlayerDataResource.Score >= PlayerManager.Instance.PlayerDataResource.GeneralUpgrade.GetCurrentUpgradePrice())
            {
                SoundManager.Instance.EmitSignal(SoundManager.SignalName.PlaySound, (int)ESoundType.Buy);
                PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.UpgradeApply, (int)EUpgradeType.General);
                foreach (TextureButton upgradeButton in _upgradeButtonsContainer.GetChildren())
                {
                    if (upgradeButton.GetIndex() == 0) continue;
                    upgradeButton.Disabled = false;
                }
            }
        };
        _upgradeButtonsContainer.GetNode<TextureButton>("UpgradeEngineButton").Pressed += async () =>
        {
            if (PlayerManager.Instance.PlayerDataResource.Score >= PlayerManager.Instance.PlayerDataResource.EngineUpgrade.GetCurrentUpgradePrice())
            {
                SoundManager.Instance.EmitSignal(SoundManager.SignalName.PlaySound, (int)ESoundType.Buy);
                PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.UpgradeApply, (int)EUpgradeType.Engine);
            }
        };
        _upgradeButtonsContainer.GetNode<TextureButton>("UpgradeGunButton").Pressed += async () =>
        {
            if (PlayerManager.Instance.PlayerDataResource.Score >= PlayerManager.Instance.PlayerDataResource.GunUpgrade.GetCurrentUpgradePrice())
            {
                SoundManager.Instance.EmitSignal(SoundManager.SignalName.PlaySound, (int)ESoundType.Buy);
                PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.UpgradeApply, (int)EUpgradeType.Gun);
            }
        };
        _upgradeButtonsContainer.GetNode<TextureButton>("UpgradeGearButton").Pressed += async () =>
        {
            if (PlayerManager.Instance.PlayerDataResource.Score >= PlayerManager.Instance.PlayerDataResource.GearUpgrade.GetCurrentUpgradePrice())
            {
                SoundManager.Instance.EmitSignal(SoundManager.SignalName.PlaySound, (int)ESoundType.Buy);
                PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.UpgradeApply, (int)EUpgradeType.Gear);
            }
        };
        _upgradeButtonsContainer.GetNode<TextureButton>("UpgradeShieldButton").Pressed += async () =>
        {
            if (PlayerManager.Instance.PlayerDataResource.Score >= PlayerManager.Instance.PlayerDataResource.ShieldUpgrade.GetCurrentUpgradePrice())
            {
                SoundManager.Instance.EmitSignal(SoundManager.SignalName.PlaySound, (int)ESoundType.Buy);
                PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.UpgradeApply, (int)EUpgradeType.Shield);
            }
        };

        PlayerManager.Instance.UpgradeApplied += (EUpgradeType upgradeType) =>
        {
            TextureButton upgradeButton = null;
            UpgradeData upgradeInfo = null;
            switch (upgradeType)
            {
                case EUpgradeType.General:
                    upgradeButton = _upgradeButtonsContainer.GetNode<TextureButton>("UpgradeGeneralButton");
                    upgradeInfo = PlayerManager.Instance.PlayerDataResource.GeneralUpgrade;
                    if (upgradeInfo.CurrentUpgradeLevel == 4) upgradeButton.Disabled = true;
                    else return;
                    break;
                case EUpgradeType.Engine:
                    upgradeButton = _upgradeButtonsContainer.GetNode<TextureButton>("UpgradeEngineButton");
                    upgradeInfo = PlayerManager.Instance.PlayerDataResource.EngineUpgrade;
                    break;
                case EUpgradeType.Gun:
                    upgradeButton = _upgradeButtonsContainer.GetNode<TextureButton>("UpgradeGunButton");
                    upgradeInfo = PlayerManager.Instance.PlayerDataResource.GunUpgrade;
                    break;
                case EUpgradeType.Gear:
                    upgradeButton = _upgradeButtonsContainer.GetNode<TextureButton>("UpgradeGearButton");
                    upgradeInfo = PlayerManager.Instance.PlayerDataResource.GearUpgrade;
                    break;
                case EUpgradeType.Shield:
                    upgradeButton = _upgradeButtonsContainer.GetNode<TextureButton>("UpgradeShieldButton");
                    upgradeInfo = PlayerManager.Instance.PlayerDataResource.ShieldUpgrade;
                    break;
            }
            if (upgradeInfo.CurrentUpgradeLevel >= PlayerManager.Instance.PlayerDataResource.GeneralUpgrade.CurrentUpgradeLevel)
            {
                upgradeButton.Disabled = true;
            }
        };
    }

    private async void OnPlayButtonPressed()
    {
        SoundManager.Instance.EmitSignal(SoundManager.SignalName.PlaySound, (int)ESoundType.Click);
        SceneManager.Instance.EmitSignal(SceneManager.SignalName.Change);

        await ToSignal(SceneManager.Instance, SceneManager.SignalName.AllowSceneTransition);

        UIManager.Instance.EmitSignal(UIManager.SignalName.HideUIElement, "UpgradeMenu");
        UIManager.Instance.EmitSignal(UIManager.SignalName.EnableUIElement, "GameInterface");

        LevelManager.Instance.EmitSignal(LevelManager.SignalName.ResetLevel);
    }

}
