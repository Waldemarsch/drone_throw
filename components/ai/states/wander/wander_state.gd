extends State

class_name WanderState

@export var _timer_time : float = 3


var _direction : Vector2 = Vector2.ZERO
var _timer : Timer


func enter(info: Dictionary = {}):
	_timer = Timer.new()
	_timer.timeout.connect(_on_timer_timeout)
	add_child(_timer)
	_on_timer_timeout()
	_timer.start(_timer_time)
	
func exit():
	if _timer.timeout.is_connected(_on_timer_timeout):
		_timer.timeout.disconnect(_on_timer_timeout)

func _on_timer_timeout():
	_direction = Vector2.from_angle(randf_range(0, TAU))
	_timer.start(_timer_time)
	
func process_physics(delta: float):
	var owner : CharacterBody2D = manager.get_parent()
	var direction_angle = _direction.angle()
	manager.movement_component.update_rotation(_direction, delta)
	manager.movement_component.move(Vector2.from_angle(owner.rotation))
	
