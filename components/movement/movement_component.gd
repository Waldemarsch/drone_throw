extends Node

class_name MovementComponent

var owner_character : Entity

@export var speed : float = 100
@export var rotation_speed : float = 50

func _ready() -> void:
	owner_character = get_parent()

func move(direction: Vector2):
	owner_character.velocity = direction * speed
	
func update_rotation(rotation: float, delta: float):
	lerp_angle(owner_character.rotation, rotation, delta * rotation_speed)
