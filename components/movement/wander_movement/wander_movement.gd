extends Node

class_name WanderMovement

@export var speed : float

var direction : Vector2


func _ready() -> void:
	owner = self.get_parent()
	assert(owner is CharacterBody2D, "Owner must be a CharacterBody2D")
	
func _physics_process(delta: float) -> void:
	owner.velocity = direction * speed
	

func _on_timer_timeout() -> void:
	direction = Vector2.RIGHT.rotated(randf_range(0, TAU)) # Replace with function body.
