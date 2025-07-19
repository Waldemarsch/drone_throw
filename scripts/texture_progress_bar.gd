extends TextureProgressBar

@onready var player : Player

func _ready() -> void:
	GameManager.player_changed.connect(_on_player_changed)

func _process(delta: float) -> void:
	if player:
		update()
	
func update():
	value = player.evolution_progress

func _on_player_changed():
	player = GameManager.get_player()
