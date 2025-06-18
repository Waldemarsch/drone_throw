extends CharacterBody2D

const SPEED = 100.0

func _process(delta):
	move_player()
	
	
func move_player():
	var direction: Vector2 = Vector2(Input.get_axis("left", "right"), Input.get_axis("up", "down")).normalized()
	velocity = direction * SPEED
	move_and_slide()
	
