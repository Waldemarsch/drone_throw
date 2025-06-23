extends CharacterBody2D

class_name Player

const SPEED = 100.0

@onready var anim_player = $AnimatedSprite2D

@onready var evolution_progress : int = 5


func _ready() -> void:
	GameManager.set_player(self)


func _process(delta):
	move_player()
	
	
func move_player():
	velocity = Vector2(Input.get_axis("left", "right"), Input.get_axis("up", "down")).normalized() * SPEED
	if velocity.length() != 0:
		anim_player.play("moving")
		move_and_slide()
	else:
		anim_player.play("idle")
		
	
