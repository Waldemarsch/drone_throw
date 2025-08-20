using Godot;
using Godot.Collections;

public partial class Main : Node2D
{
    bool is_changing = false;
    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {
        if (!is_changing)
        {
            is_changing = true;
            GameManager.Instance.ChangeSceneTo("res://scenes/main_menu.tscn");   
        }
    }

}