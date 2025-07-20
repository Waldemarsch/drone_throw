extends RefCounted

class_name MovementStrategy

var movement_component : Node

func enter():
	pass
	
func exit():
	pass
	
func process_movement(delta: float) -> Vector2:
	push_error("process_movement is not implemented in current strategy")
	return Vector2.ZERO
