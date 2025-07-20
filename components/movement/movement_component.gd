extends Node

class_name MovementComponent

@export var initial_movement_strategy : Script

var curr_strategy : MovementStrategy


func _ready() -> void:
	if initial_movement_strategy:
		var new_strategy_instance = initial_movement_strategy.new()
		set_strategy(new_strategy_instance)
	else:
		push_warning("MovementComponent has no initial strategy assigned.")
		
func _physics_process(delta: float) -> void:
	if curr_strategy:
		owner = get_parent()
		owner.velocity = curr_strategy.process_movement(delta)
		owner.move_and_slide()
		
func set_strategy(new_strategy: MovementStrategy):
	if curr_strategy:
		curr_strategy.exit()
		
	curr_strategy = new_strategy
	
	curr_strategy.movement_component = self
	
	curr_strategy.enter()
