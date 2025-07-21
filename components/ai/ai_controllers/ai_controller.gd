extends Node

class_name AIController

@export var initial_state : State
@export var sensor : Area2D
@export var timer_time : float = 0.3

var _timer : Timer

var _curr_state : State

var _states : Dictionary = {}

func _ready() -> void:
	for child in get_children():
		if child is State:
			_states[child.name] = child
			child.manager = self
			child.set_physics_process(false)
	
	assert(not _states.is_empty(), "BehaviorManager has no child State nodes.")
	assert(sensor, "AIController requires a DetectionSensor on the owner.")
	
	set_state(initial_state.name)
	
	_timer = Timer.new()
	add_child(_timer)
	_timer.timeout.connect(update_brain())
	_timer.start(timer_time)
	
func update_brain():
	var targets : Array = []
	targets = find_targets_in_sensor()
	if len(targets):
		targets = sort_bodies_by_distance(targets, get_parent())
	var _new_state := select_state(targets)
	if _new_state[0] != _curr_state:
		set_state(_new_state[0].name)
	
	
func _physics_process(delta: float) -> void:
	if _curr_state:
		_curr_state.process_physics(delta)
	
	
func set_state(new_state_name: String) -> void:
	if not _states.has(new_state_name):
		push_warning("Attempted to switch to an unknown state: '%s'" % new_state_name)
		return
	
	if _curr_state:
		_curr_state.exit()
		_curr_state.set_physics_process(false)
		
	_curr_state = _states[new_state_name]
	
	_curr_state.set_physics_process(true)
	_curr_state.enter()
	
func find_targets_in_sensor() -> Array[Node2D]:
	return sensor.get_overlapping_bodies()

func sort_bodies_by_distance(bodies: Array, reference_node: Node2D) -> Array:
	bodies.sort_custom(
		func(a: Node2D, b: Node2D):
			var distance_a_sq = a.global_position.distance_squared_to(reference_node.global_position)
			var distance_b_sq = b.global_position.distance_squared_to(reference_node.global_position)
			return distance_a_sq < distance_b_sq
	)
	return bodies

func select_state(bodies: Array[Node2D]) -> Array:
	return []
	
