extends TextureProgressBar

@export var player : Player

func _process(delta: float) -> void:
	update()
	
func update():
	value = player.evolution_progress
