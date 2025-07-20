extends AiComponent

@export var movement_component : MovementComponent
@export var sensor : Area2D

@onready var _timer : Timer = $Timer

enum STRATEGY_SET {CHASE, WANDER, FLEE}

var curr_strategy : STRATEGY_SET = STRATEGY_SET.WANDER

func _on_timer_timeout() -> void:
	if movement_component and sensor:
		choose_strategy()
		
func choose_strategy() -> void:
	var scanned_body = _find_body_in_sensor()
	if not scanned_body:
		curr_strategy = STRATEGY_SET.WANDER
	else:
		if scanned_body.is_in_group("herbivores"):
			curr_strategy = STRATEGY_SET.CHASE
		else:
			pass
	
func _find_body_in_sensor() -> Node2D:
	var bodies = sensor.get_overlapping_bodies()
	for body in bodies:
		if body:
			return body
	return null
