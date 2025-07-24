extends State

class_name ChaseState

var _target : Node2D

func enter(info: Dictionary = {}):
	if info.has("prey"):
		_target = info["prey"]
	else:
		manager.set_state(manager.initial_state.name)
	
func exit():
	pass
	
func process_physics(delta: float):
	var owner : CharacterBody2D = manager.get_parent()
	var direction_to_target = owner.global_position.direction_to(_target.global_position)
	manager.movement_component.update_rotation(direction_to_target, delta)
	manager.movement_component.move(Vector2.from_angle(owner.rotation))
