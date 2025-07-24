extends Node

class_name MovementComponent

var owner_character : CharacterBody2D
@export var owner_anim_node : DragonBonesArmatureView


@export var speed : float = 100
@export var rotation_speed : float = 1.5

func _ready() -> void:
	owner_character = get_parent()

func move(direction: Vector2):
	owner_character.velocity = direction * speed
	if owner_anim_node.current_animation != "move":
		owner_anim_node.current_animation = "move"
	owner_character.move_and_slide()
	
func update_rotation(direction: Vector2, delta: float):
	owner_character.rotation = lerp_angle(owner_character.rotation, direction.angle(), delta * rotation_speed)
