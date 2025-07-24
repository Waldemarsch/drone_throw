extends CharacterBody2D

class_name Entity

func _ready() -> void:
	GameManager.set_player(self)
