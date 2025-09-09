using Godot;
using System.Linq;

public partial class GunUpgrade : Upgrade
{
    private enum GunState { Searching, Aiming, Firing, Reloading }
    private GunState _currentState = GunState.Searching;
    
    [Export]
    private PackedScene _bulletScene; // Сцена пули
    [Export(PropertyHint.Range, "0, 20")]
    private float _rotationSpeed = 5.0f;
    [Export]
    private float _reloadTime = 1.0f;

    private Area2D _detectionRange;
    private Marker2D _muzzle;
    private Timer _reloadTimer;
    private Node2D _target;

    public override void _Ready()
    {
        base._Ready();

        _detectionRange = GetNode<Area2D>("DetectionRange");

        _muzzle = GetNode<Marker2D>("Muzzle");
        _reloadTimer = GetNode<Timer>("ReloadTimer");
        _reloadTimer.WaitTime = _reloadTime;
        _reloadTimer.Timeout += OnReloadFinished;
    }

    public override void _Process(double delta)
    {
        switch (_currentState)
        {
            case GunState.Searching:
                FindTarget();
                break;
            
            case GunState.Aiming:
                AimAtTarget((float)delta);
                break;
                
            // Состояния Firing и Reloading управляются таймером, в _Process делать нечего
            case GunState.Firing:
            case GunState.Reloading:
                break;
        }
    }
    
    private void FindTarget()
    {
        // Получаем всех мобов в радиусе, которые в группе "enemies"
        var potentialTargets = _detectionRange.GetOverlappingBodies().ToList();

        if (potentialTargets.Count > 0)
        {
            // Находим ближайшего
            _target = potentialTargets.OrderBy(t => t.GlobalPosition.DistanceTo(this.GlobalPosition)).First();
            _currentState = GunState.Aiming;
        }
    }

    private void AimAtTarget(float delta)
    {
        if (!IsInstanceValid(_target))
        {
            _target = null;
            _currentState = GunState.Searching;
            return;
        }
        
        // Получаем угол до цели
        float angleToTarget = this.GlobalPosition.AngleToPoint(_target.GlobalPosition);
        // Плавно поворачиваемся к цели
        this.GlobalRotation = Mathf.LerpAngle(this.GlobalRotation, angleToTarget, _rotationSpeed * delta);

        // Если мы почти наведены на цель - стреляем
        if (Mathf.IsEqualApprox(this.GlobalRotation, angleToTarget, 0.1f))
        {
            _currentState = GunState.Firing;
            Fire();
        }
    }

    private void Fire()
    {
        if (!IsInstanceValid(_target))
        {
            _currentState = GunState.Searching;
            return;
        }

        GD.Print("ОГОНЬ!");
        var bullet = (Bullet)_bulletScene.Instantiate<Bullet>().Duplicate();
        bullet.GlobalPosition = _muzzle.GlobalPosition;
        bullet.GlobalRotation = this.GlobalRotation;
        GetNode<Node>("Node").AddChild(bullet);
        bullet.Launch(_target.GlobalPosition);

        _currentState = GunState.Reloading;
        _reloadTimer.Start();
    }

    private void OnReloadFinished()
    {
        // После перезарядки снова ищем цель
        _currentState = GunState.Searching;
    }
}