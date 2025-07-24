extends State

class_name FleeState

var _target : Node2D

var state_name : String = "FleeState"

func enter(info: Dictionary = {}):
	if info.has("body"):
		_target = info["body"]
	else:
		manager.set_state(manager.initial_state.name)
	
func exit():
	pass
	
func process_physics(delta: float):
	var owner : CharacterBody2D = manager.get_parent()
	var direction_to_target = owner.global_position.direction_to(_target.global_position)
	manager.movement_component.update_rotation(-direction_to_target, delta)
	manager.movement_component.move(Vector2.from_angle(owner.rotation))
	
	
