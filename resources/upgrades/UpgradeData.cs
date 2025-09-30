using Godot;
using Godot.Collections;


[GlobalClass]
public partial class UpgradeData : Resource
{
    [Export] public int CurrentUpgradeLevel = 0;
    [Export] public Array<int> UpgradePrices;

    public int GetCurrentUpgradePrice()
    {
        return UpgradePrices[CurrentUpgradeLevel];
    }
}