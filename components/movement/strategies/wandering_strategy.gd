extends MovementStrategy

class_name WanderingStrategy

@export var wander_time : float
@export var stop_chance : float

var _direction := Vector2.ZERO
var _timer: Timer

func enter():
	_timer = Timer.new()
	movement_component.add_child(_timer)
	_timer.timeout.connect(_on_timer_timeout)
	_on_timer_timeout()
	

func exit():
	if is_instance_valid(_timer):
		_timer.queue_free()
		
func process_movement(delta: float):
	var owner = movement_component.get_parent()
	return _direction * owner.speed

func _on_timer_timeout():
	if randf() < stop_chance:
		_direction = Vector2.ZERO
	else:
		_direction = Vector2.RIGHT.rotated(randf_range(0, TAU))
	_timer.start(wander_time)
