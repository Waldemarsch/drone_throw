using Godot;

public partial class Main : Node // Убедись, что имя класса совпадает с именем твоего узла
{
    private int _health = 100;
    private string _characterName = "Player";
    private bool _isReady = false;

    public override void _Ready()
    {
        GD.Print($"'{_characterName}' is getting ready...");
        _isReady = true;
        TakeDamage(25);
    }

    private void TakeDamage(int amount)
    {
        _health -= amount; // <--- Сюда мы поставим точку останова
        GD.Print($"'{_characterName}' took {amount} damage. Health is now {_health}.");
    }
}