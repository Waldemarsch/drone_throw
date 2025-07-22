extends Node

@export var spawn_time : float = 10.0

var _timer : Timer

func _ready() -> void:
	_timer = Timer.new()
	add_child(_timer)
	_timer.timeout.connect(_on_timer_timeout())
	_timer.start(spawn_time)
	
func _on_timer_timeout():
	pass
