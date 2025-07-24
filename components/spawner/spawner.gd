extends Node

@export var level_data : LevelData
@export var entities_container_path : NodePath

@export var spawn_time : float = 1
@export var spawn_count : int = 5

@export var min_spawn_radius : float = 600.0
@export var max_spawn_radius : float = 1200.0

var _timer : Timer

var _entities_container : Node2D

func _ready() -> void:
	
	_entities_container = get_node(entities_container_path)
	
	assert(level_data, "Level data is not presented")
	assert(_entities_container, "Entities container is not presented")
	
	_timer = Timer.new()
	add_child(_timer)
	_timer.timeout.connect(_on_timer_timeout)
	_timer.start(spawn_time)
	
func _on_timer_timeout():
	var ent_count = _entities_container.get_child_count()
	if ent_count >= level_data.max_population:
		return
	var player = GameManager.get_player()
	for i in range(spawn_count):
		var ent_to_spawn : Node2D
		if randf() <= 0.7:
			ent_to_spawn = level_data.herbivores.pick_random().instantiate()
		else:
			ent_to_spawn = level_data.carnivores.pick_random().instantiate()
		var ent_spawn_angle = randf_range(0, TAU)
		var ent_spawn_radius = randf_range(min_spawn_radius, max_spawn_radius)
		ent_to_spawn.global_position = player.global_position + Vector2.from_angle(ent_spawn_angle) * ent_spawn_radius
		
		_entities_container.add_child(ent_to_spawn)
