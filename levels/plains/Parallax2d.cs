using Godot;
using System;
using System.Numerics;

public partial class Parallax2d : Parallax2D
{
    // Ссылка на основную камеру игры
    private Camera2D _camera;

    public override void _Ready()
    {
        // Находим камеру при запуске. 
        // Путь может отличаться в вашем проекте.
        // Один из способов - сделать камеру текущей и найти ее так:
        _camera = GetViewport().GetCamera2D();
    }

    public override void _Process(double delta)
    {
        if (_camera != null)
        {
            // Это главная строка. Мы говорим фону:
            // "Твой масштаб всегда должен быть таким же, как зум камеры".
            this.Scale = Godot.Vector2.One / _camera.Zoom;
        }
    }
}
