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
	
func physics_process(delta: float):
	var owner : CharacterBody2D = manager.get_parent()
	var direction_to_target = owner.global_position.direction_to(_target.global_position)
	var direction_angle = direction_to_target.angle()
	manager.movement_component.update_rotation(-direction_angle, delta)
	manager.movement_component.move(Vector2(0, owner.rotation))
	
	
