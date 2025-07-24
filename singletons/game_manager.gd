extends Node

@onready var player : Player = null

signal player_changed()

func set_player(new_player):
	player = new_player
	player_changed.emit()
	
	
func get_player():
	return player
