extends Node

class_name MovementComponent

var owner_character : Entity
@export var owner_anim_node : DragonBonesArmatureView


@export var speed : float = 100
@export var rotation_speed : float = 300

func _ready() -> void:
	owner_character = get_parent()

func move(direction: Vector2):
	owner_character.velocity = direction * speed
	if owner_character.velocity.length() == 0:
		owner_anim_node.current_animation = "[none]"
	elif owner_anim_node.current_animation != "move":
		owner_anim_node.current_animation = "move"
	
func update_rotation(rotation: float, delta: float):
	lerp_angle(owner_character.rotation, rotation, delta * rotation_speed)
