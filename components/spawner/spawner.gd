extends Node

@export var level_data : LevelData
@export var entities_container_path : NodePath

@export var spawn_time : float = 10.0
@export var spawn_count : int = 5

var _timer : Timer

var _entities_container : Node2D

func _ready() -> void:
	
	_entities_container = get_node(entities_container_path)
	
	assert(level_data, "Level data is not presented")
	assert(_entities_container, "Entities container is not presented")
	
	_timer = Timer.new()
	add_child(_timer)
	_timer.timeout.connect(_on_timer_timeout())
	_timer.start(spawn_time)
	
func _on_timer_timeout():
	var ent_count = _entities_container.get_child_count()
	if ent_count >= level_data.max_population:
		return
	for i in range(spawn_count):
		if randf() <= 0.7:
			pass
