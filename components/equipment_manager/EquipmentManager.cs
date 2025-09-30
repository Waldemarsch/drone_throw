using Godot;
using System;

public partial class EquipmentManager : Node
{

    private Node UpgradeContainer;

    public override void _Ready()
    {
        UpgradeContainer = GetParent().GetNode<Node>("UpgradeContainer");
    }


    public void EquipEngine(PackedScene engineScene)
    {
        var engineInstance = engineScene.Instantiate<EngineUpgrade>();


    }

    // public void DeequipEngine();
}
