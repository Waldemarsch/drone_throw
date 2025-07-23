extends State

class_name WanderState

@onready var _timer : Timer = Timer.new()
@export var _timer_time : float = 1

var _direction : Vector2 = Vector2.ZERO

func enter(info: Dictionary = {}):
	_timer.timeout.connect(_on_timer_timeout())
	
func exit():
	if _timer.timeout.is_connected(_on_timer_timeout()):
		_timer.timeout.disconnect(_on_timer_timeout())

func _on_timer_timeout():
	_direction = Vector2(0, randf_range(0, TAU))
	_timer.start(_timer_time)
	
func process_physics(delta: float):
	var owner : CharacterBody2D = manager.get_parent()
	var direction_angle = _direction.angle()
	manager.movement_component.update_rotation(direction_angle, delta)
	manager.movement_component.move(Vector2(0, owner.rotation))
	
